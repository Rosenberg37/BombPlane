using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BombPlane
{
    public class AI
    {
        public AI(int port)
        {
            _port = port;
        }

        public void Start()
        {
            try
            {
                _socketListen.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, -300);

                //1.绑定IP和Port
                IPEndPoint iPEndPoint = new(IPAddress.Any, _port);
                //2.使用Bind()进行绑定
                _socketListen.Bind(iPEndPoint);
                //3.开启监听
                _socketListen.Listen(256); //Listen(int backlog); backlog:监听数量 

                //开启线程Accept进行通信的客户端socket
                _threadListen = new Thread(Listen)
                {
                    IsBackground = true    //运行线程在后台执行
                };   //线程绑定Listen函数
                _threadListen.Start();    //Start里面的参数是Listen函数所需要的参数，这里传送的是用于通信的Socket对象
            }
            catch (SocketException)
            {
                MessageBox.Show("AI程序启动失败");
            }
        }

        private void Listen() //建立与客户端的连接
        {
            List<Thread> threads = new();

            try
            {
                while (true)
                {
                    //4.阻塞到有client连接，得到用于socket的连接
                    Socket socketAccept = _socketListen.Accept();

                    //开启第二个线程接收客户端数据
                    /*
                    * tip：
                    * Accept会阻碍主线程的运行，一直在等待客户端的请求，
                    * 客户端如果不接入，它就会一直在这里等着，主线程卡死
                    * 所以开启一个新线程接收客户单请求
                    */
                    Thread acceptThread = new(Accept)
                    {
                        IsBackground = true    //运行线程在后台执行
                    };   //线程绑定Accept函数
                    acceptThread.Start(socketAccept);  //Start里面的参数是Listen函数所需要的参数，这里传送的是用于通信的Socket对象
                    threads.Add(acceptThread);
                }
            }
            catch (ThreadInterruptedException)
            {
                foreach (var thread in threads)
                    thread.Interrupt();
            }
            catch (SocketException) { }
        }

        private void Accept(object? socket) //接收客户端数据
        {
            Socket acceptSocket = socket as Socket;
            try
            {
                while (true)
                {
                    string command = Receive(acceptSocket);
                    string[] strs = command.Split(' ');
                    switch (strs[0])
                    {
                        case "StartGame":
                            Send(acceptSocket, "GameAccept");
                            break;
                        case "FinishPrepare":
                            Prepare();
                            Send(acceptSocket, "FinishPrepare");
                            break;
                        case "GetBombResult":
                            int Y = strs[1][0] - 'A';
                            int X = strs[1][1] - '0';
                            BombResult bombResult = Bomb(X, Y);
                            Send(acceptSocket, "BombResult " + strs[1] + " " + bombResult.ToString());

                            char newY = (char)(random.Next(0, 9) + 'A');
                            char newX = (char)(random.Next(0, 9) + '0');
                            Send(acceptSocket, string.Format("GetBombResult {0}{1}", newY, newX));
                            break;
                        case "BombResult":
                            BombResult result = (BombResult)Enum.Parse(typeof(BombResult), strs[2], true);
                            if (result == BombResult.head)
                                _numOfHitPlane++;
                            if (_numOfHitPlane == GridNetwork.NumOfPlane)
                            {
                                Send(acceptSocket, "GameOver");
                                TurnIdle();
                            }
                            break;
                        case "GameOver":
                            TurnIdle();
                            break;
                        case "GetName":
                            Send(acceptSocket, "AI");
                            break;
                        case "Connect":
                            Send(acceptSocket, "Accept");
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (ReceiveExpection) { }
        }

        public void Prepare()
        {
            int columnCount = GridNetwork.ColumnCount;
            int rowCount = GridNetwork.RowCount;
            _planes = new Plane[GridNetwork.NumOfPlane] {
                new Plane((Direction)random.Next(0, 3), random.Next(0, columnCount), random.Next(0, rowCount)),
                new Plane((Direction)random.Next(0, 3), random.Next(0, columnCount), random.Next(0, rowCount)),
                new Plane((Direction)random.Next(0, 3), random.Next(0, columnCount), random.Next(0, rowCount)),
            };
            int index = 0;
            while (!GridNetwork.CheckPlanesValid(_planes))
                _planes[index++ % GridNetwork.NumOfPlane] = new Plane((Direction)random.Next(0, 3), random.Next(0, columnCount), random.Next(0, rowCount));
        }

        public BombResult Bomb(Point point)
        {
            foreach (Plane plane in _planes)
                if (point.X == plane.HeadX && point.Y == plane.HeadY)
                    return BombResult.head;
                else
                    foreach (Point planePoint in plane.Points)
                        if (point.X == planePoint.X && point.Y == planePoint.Y)
                            return BombResult.body;
            return BombResult.none;
        }

        private void TurnIdle()
        {
            state = GameState.idle;
            _numOfHitPlane = 0;
        }

        public BombResult Bomb(int X, int Y)
        {
            foreach (Plane plane in _planes)
                if (X == plane.HeadX && Y == plane.HeadY)
                    return BombResult.head;
                else
                    foreach (Point planePoint in plane.Points)
                        if (X == planePoint.X && Y == planePoint.Y)
                            return BombResult.body;
            return BombResult.none;
        }

        private static string Receive(Socket socket) //接收客户端数据
        {
            try
            {
                byte[] buffer = new byte[1024];
                int len = socket.Receive(buffer);
                return Encoding.Unicode.GetString(buffer, 0, len);
            }
            catch (SocketException e) { throw new ReceiveExpection(e); }
        }

        private static void Send(Socket socket, string text) // 向客户端发送数据
        {
            try
            {
                byte[] buffer = Encoding.Unicode.GetBytes(text);    //将字符串转成字节格式发送
                socket.Send(buffer);  //调用Send()向客户端发送数据
            }
            catch (SocketException e) { throw new SendException(e); }
        }

        private int _port;
        private Plane[]? _planes;
        private Random random = new();
        private int _numOfHitPlane;
        private GameState state = GameState.idle;

        private Socket _socketListen = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);    //声明用于监听的套接字;
        private Thread? _threadListen;     //声明线程
    }
}
