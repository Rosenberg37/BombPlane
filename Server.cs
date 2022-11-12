using System.Net.Sockets;
using System.Net;

namespace BombPlane
{
    public class ConnectedEventArgs : EventArgs
    {
        public string? RivalName { get; set; }
        public Socket? CommunicateSocket { get; set; }
    }
    public delegate void ConnectedEventHandler(object sender, EventArgs e);

    public class Server
    {
        public Server(string UserName, string Watchword, string WatchwordHint, int ListenPort)
        {
            this.UserName = UserName;
            this.Watchword = Watchword;
            this.WatchwordHint = WatchwordHint;

            listenCTS = new CancellationTokenSource();
            listenCTS.Token.Register(() =>
            {
                listenThread = null;
                listenSocket.Close();
                listenSocket = null;
            });

            this.ListenPort = ListenPort;
        }

        public string UserName;
        public string Watchword;
        public string WatchwordHint;

        private Socket? listenSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);    //声明用于监听的套接字;
        private Thread? listenThread;     //声明线程
        private readonly CancellationTokenSource? listenCTS;
        public Socket? AcceptSocket;

        private int _listenPort = -1;
        public int ListenPort
        {
            get { return _listenPort; }
            set
            {
                try
                {
                    if (listenThread != null)
                        listenCTS.Cancel();

                    listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, -300);

                    //1.绑定IP和Port
                    IPEndPoint iPEndPoint = new(IPAddress.Any, value);
                    //2.使用Bind()进行绑定
                    listenSocket.Bind(iPEndPoint);
                    //3.开启监听
                    listenSocket.Listen();

                    //开启线程Accept进行通信的客户端socket
                    listenThread = new Thread(Listen)
                    {
                        IsBackground = true    //运行线程在后台执行
                    };   //线程绑定Listen函数
                    listenThread.Start(listenCTS.Token);    //Start里面的参数是Listen函数所需要的参数

                    _listenPort = value;
                }
                catch (SocketException)
                {
                    MessageBox.Show("监听程序启动失败，请尝试重新设置监听端口");
                }
            }
        }

        public event ConnectedEventHandler Connected;

        private void Listen(object obj) //建立与客户端的连接
        {
            CancellationToken ct = (CancellationToken)obj;

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    //4.阻塞到有client连接，得到用于socket的连接
                    Socket newSocket = listenSocket.Accept();
                    if (AcceptSocket != null)
                        Utility.Send(newSocket, "Busy");
                    else
                    {
                        string command = Utility.Receive(newSocket);
                        string[] strs = command.Split(' ');
                        switch (strs[0])
                        {
                            case "GetName":
                                Utility.Send(newSocket, UserName);
                                break;
                            case "Connect":
                                if (WatchwordHint.Length > 0 && Watchword.Length > 0)
                                {
                                    Utility.Send(newSocket, "WatchwordHint " + WatchwordHint);
                                    if (Utility.Receive(newSocket) == Watchword)
                                    {
                                        Utility.Send(newSocket, "Accept");
                                        string rivalName = strs[1];

                                        ConnectedEventArgs args = new()
                                        {
                                            RivalName = rivalName,
                                            CommunicateSocket = newSocket
                                        };
                                        Connected(this, args);
                                        MessageBox.Show("收到来自" + rivalName + "的连接请求，对方口令验证通过并连接成功");
                                    }
                                    else
                                    {
                                        Utility.Send(newSocket, "Reject");
                                        newSocket.Close();
                                        return;
                                    }
                                }
                                else
                                {
                                    Utility.Send(newSocket, "Accept");
                                    string rivalName = strs[1];
                                    ConnectedEventArgs args = new()
                                    {
                                        RivalName = rivalName,
                                        CommunicateSocket = newSocket
                                    };
                                    Connected(this, args);
                                    MessageBox.Show("收到来自" + rivalName + "的连接请求，自动连接成功");
                                }
                                break;
                            case "Close":
                                newSocket.Close();
                                break;
                            default:
                                Utility.Send(newSocket, "Unexpected");
                                break;
                        }
                    }
                }
                catch (SocketException) { }
            }
        }

        public void Start()
        {
            try
            {
                if (listenThread != null)
                    listenCTS.Cancel();

                listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, -300);

                //1.绑定IP和Port
                IPEndPoint iPEndPoint = new(IPAddress.Any, _listenPort);
                //2.使用Bind()进行绑定
                listenSocket.Bind(iPEndPoint);
                //3.开启监听
                listenSocket.Listen();

                //开启线程Accept进行通信的客户端socket
                listenThread = new Thread(Listen)
                {
                    IsBackground = true    //运行线程在后台执行
                };   //线程绑定Listen函数
                listenThread.Start(listenCTS.Token);    //Start里面的参数是Listen函数所需要的参数
            }
            catch (SocketException)
            {
                MessageBox.Show("监听程序启动失败");
            }
        }
    }
}
