using System.Net;
using System.Net.Sockets;


namespace BombPlane
{
    using State = HashSet<Plane>;
    using StateSet = HashSet<HashSet<Plane>>;
    using StateMapping = Dictionary<BombResult, HashSet<HashSet<Plane>>>;

    public class AI
    {

        private static StateSet _allState = new StateSet();
        public static StateSet AllState
        {
            get
            {
                if (_allState.Count == 0)
                    foreach (var plane1 in Plane.BoundedPlanes)
                        foreach (var plane2 in Plane.BoundedPlanes)
                            foreach (var plane3 in Plane.BoundedPlanes)
                                if (!plane2.Conflict(plane1) && !plane3.Conflict(plane2) && !plane3.Conflict(plane1))
                                    _allState.Add(new State(new Plane[] { plane1, plane2, plane3 }));
                return _allState;
            }
        }


        private static StateMapping[,] _stateMapping;
        public static StateMapping[,] StateMapping
        {
            get
            {
                if (_stateMapping == null)
                {
                    _stateMapping = new StateMapping[GridView.ColumnCount, GridView.RowCount];
                    for (int x = 0; x < GridView.ColumnCount; x++)
                    {
                        for (int y = 0; y < GridView.RowCount; y++)
                        {
                            _stateMapping[x, y] = new();
                            _stateMapping[x, y][BombResult.none] = new StateSet();
                            _stateMapping[x, y][BombResult.body] = new StateSet();
                            _stateMapping[x, y][BombResult.head] = new StateSet();
                        }
                    }

                    foreach (var state in AllState)
                    {
                        HashSet<Point> points = GridView.Points;
                        foreach (var plane in state)
                        {
                            _stateMapping[plane.HeadPoint.X, plane.HeadPoint.Y][BombResult.head].Add(state);
                            foreach (var point in plane.BodyPoints)
                                _stateMapping[point.X, point.Y][BombResult.body].Add(state);
                            points.ExceptWith(plane.Points);
                        }
                        foreach (var point in points)
                            _stateMapping[point.X, point.Y][BombResult.none].Add(state);
                    }
                }
                return _stateMapping;
            }

        }

        public AI(int port)
        {
            _port = port;
        }

        private int _port;
        private Plane[]? _planes;
        private Random random = new();
        private int numOfHitPlane;
        private GameState state = GameState.idle;

        private Socket _socketListen = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);    //声明用于监听的套接字;
        private Thread? _threadListen;     //声明线程

        private HashSet<Point> remainPoints;
        private StateSet remianState;
        private Point selectedPoint;

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
                    string command = Utility.Receive(acceptSocket);
                    string[] strs = command.Split(' ');
                    switch (strs[0])
                    {
                        case "StartGame":
                            Utility.Send(acceptSocket, "GameAccept");
                            break;
                        case "FinishPrepare":
                            Prepare();
                            Utility.Send(acceptSocket, "FinishPrepare");
                            break;
                        case "GetBombResult":
                            Point bombPoint = GridView.ConvertStringToPoint(strs[1]);
                            BombResult bombResult = Bomb(bombPoint);
                            Utility.Send(acceptSocket, "BombResult " + strs[1] + " " + bombResult.ToString());

                            SelectPoint();
                            string pos = GridView.ConvertPointToString(selectedPoint);
                            Utility.Send(acceptSocket, string.Format("GetBombResult {0}", pos));
                            break;
                        case "BombResult":
                            BombResult result = (BombResult)Enum.Parse(typeof(BombResult), strs[2], true);
                            StateSet states = StateMapping[selectedPoint.X, selectedPoint.Y][result];
                            remianState.IntersectWith(states);

                            if (result == BombResult.head)
                                numOfHitPlane++;
                            if (numOfHitPlane == GridView.NumOfPlane)
                            {
                                Utility.Send(acceptSocket, "GameOver");
                                TurnIdle();
                            }
                            break;
                        case "GameOver":
                            TurnIdle();
                            break;
                        case "GetName":
                            Utility.Send(acceptSocket, "AI");
                            break;
                        case "Connect":
                            Utility.Send(acceptSocket, "Accept");
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (ReceiveExpection) { }
        }

        private Point SelectPoint()
        {
            double maxGain = -1;
            foreach (var point in remainPoints)
            {
                StateSet noneStates = StateMapping[point.X, point.Y][BombResult.none];
                StateSet possibleNoneStates = new(remianState.Intersect(noneStates));
                double p_none = (double)possibleNoneStates.Count / remianState.Count;

                StateSet bodyStates = StateMapping[point.X, point.Y][BombResult.body];
                StateSet possibleBodyStates = new(remianState.Intersect(bodyStates));
                double p_body = (double)possibleBodyStates.Count / remianState.Count;

                StateSet headStates = StateMapping[point.X, point.Y][BombResult.none];
                StateSet possibleHeadStates = new(remianState.Intersect(headStates));
                double p_head = (double)possibleHeadStates.Count / remianState.Count;

                double gain = 1 - p_none * p_none - p_body - p_body - p_head * p_head;
                if (gain > maxGain)
                {
                    maxGain = gain;
                    selectedPoint = point;
                }
            }
            remainPoints.Remove(selectedPoint);
            return selectedPoint;
        }

        public void Prepare()
        {
            int columnCount = GridView.ColumnCount;
            int rowCount = GridView.RowCount;
            _planes = new Plane[GridView.NumOfPlane] {
                new Plane((Direction)random.Next(0, 3), random.Next(0, columnCount), random.Next(0, rowCount)),
                new Plane((Direction)random.Next(0, 3), random.Next(0, columnCount), random.Next(0, rowCount)),
                new Plane((Direction)random.Next(0, 3), random.Next(0, columnCount), random.Next(0, rowCount)),
            };
            int index = 0;
            while (!GridView.CheckPlanesValid(_planes))
                _planes[index++ % GridView.NumOfPlane] = new Plane((Direction)random.Next(0, 3), random.Next(0, columnCount), random.Next(0, rowCount));
            remainPoints = new HashSet<Point>(GridView.Points);
            remianState = new StateSet(AllState);
            numOfHitPlane = 0;
        }

        public BombResult Bomb(Point point)
        {
            return Bomb(point.X, point.Y);
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

        private void TurnIdle() { state = GameState.idle; }
    }
}
