using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Microsoft.VisualBasic;

namespace BombPlane
{
    public partial class BombPLaneForm : Form
    {
        public BombPLaneForm()
        {
            InitializeComponent();

            // Let threads able to control the MainFrom controls;
            CheckForIllegalCrossThreadCalls = false;

            ListenPort = 2000;

            ai = new AI(2001);

            FlushRivalsOnLocalNetwork();
        }

        private void MainButtunClick(object sender, EventArgs e)
        {
            switch (state)
            {
                case GameState.idle:
                    if (_communicateSocket == null)
                        MessageBox.Show("请先选择对手");
                    else
                    {
                        Send(_communicateSocket, "StartGame");
                        statusStrip.Items[2].Text = "等待对方回应";
                        MainButtun.Enabled = false;
                    }
                    break;
                case GameState.preparing:
                    if (!gridNetworkOurSide.CheckPlanesValid())
                        MessageBox.Show("当前飞机位置不合法，请避免飞机重叠");
                    else
                    {
                        Send(_communicateSocket, "FinishPrepare");
                        statusStrip.Items[2].Text = "等待对手完成准备";
                        MainButtun.Enabled = false;
                        state = GameState.gaming;
                    }
                    break;
                case GameState.gaming:
                case GameState.wait:
                    try
                    {
                        // Get our side bomb selection and draw on counterside.
                        Point selectPoint = gridNetworkCounterSide.GetSelectBombPoint();
                        char Y = (char)(gridNetworkCounterSide.SelectedButtonY + 'A');
                        char X = (char)(gridNetworkCounterSide.SelectedButtonX + '0');
                        Send(_communicateSocket, string.Format("GetBombResult {0}{1}", Y, X));
                    }
                    catch (DuplicatedSelectionException)
                    {
                        MessageBox.Show("选定轰炸位置不合理，请不要选择轰炸过的位置");
                    }
                    break;
            }
        }

        private void TurnIdle()
        {
            state = GameState.idle;
            statusStrip.Items[2].Text = "闲置";
            MainButtun.Text = "开始游戏";
            gridNetworkOurSide.PlaneVisiblibity = false;
            gridNetworkCounterSide.ClearGrids();
            _numOfHitPlane = 0;
        }

        private void StartGame()
        {
            state = GameState.preparing;
            statusStrip.Items[2].Text = "准备中";
            gridNetworkOurSide.PlaneVisiblibity = true;
            MainButtun.Text = "确认飞机位置";
        }

        private void FlushToolStripMenuItemClick(object sender, EventArgs e)
        {
            FlushRivalListToolStripMenuItemClick(sender, e);
        }

        private void ConnectToolStripMenuItemClick(object sender, EventArgs e)
        {
            ListViewItem item = listView.SelectedItems[0];
            string name = item.SubItems[0].Text;

            try
            {
                IPAddress ip = IPAddress.Parse(item.SubItems[1].Text);
                int port = int.Parse(item.SubItems[2].Text);
                IPEndPoint ipEndPoint = new IPEndPoint(ip, port);


                //声明负责通信的套接字
                Socket clientSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(ipEndPoint);
                //用socket对象的Connect()方法以上面建立的IPEndPoint对象做为参数，向服务器发出连接请求
                Send(clientSocket, "Connect " + _userName);

                string command = Receive(clientSocket);
                string[] strs = command.Split(' ');
                if (strs[0] == "WatchwordHint")
                {
                    string input = Interaction.InputBox("口令提示：" + strs[1], "请输入口令");
                    if (input.Length != 0)
                    {
                        Send(clientSocket, input);
                        string result = Receive(clientSocket);
                        if (result != "Accept")
                            MessageBox.Show("连接失败，口令错误");
                        else
                        {
                            toolStripStatusLabelLeft.Text = name + " connected";
                            if (_communicateSocket != null)
                                _communicateSocket.Close();
                            _communicateSocket = clientSocket;
                            _communicateThread = new Thread(Communicate)
                            {
                                IsBackground = true    //运行线程在后台执行
                            };   //线程绑定Listen函数
                            _communicateThread.Start();
                        }
                    }
                    else
                    {
                        Send(clientSocket, "Close");
                        MessageBox.Show("连接失败，未输入口令");
                    }
                }
                else if (strs[0] == "Busy")
                    MessageBox.Show("对方正在忙");
                else if (strs[0] == "Accept")
                {
                    toolStripStatusLabelLeft.Text = name + " connected";
                    if (_communicateSocket != null)
                        _communicateSocket.Close();
                    _communicateSocket = clientSocket;
                    _communicateThread = new Thread(Communicate)
                    {
                        IsBackground = true    //运行线程在后台执行
                    };   //线程绑定Listen函数
                    _communicateThread.Start();
                }
                else
                    MessageBox.Show("连接失败了");

            }
            catch (SocketException)
            {
                MessageBox.Show("无法连接目标客户端");
            }
        }

        private void FlushRivalListToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_flushThread == null)
            {
                _flushThread = new Thread(() => { FlushRivalsOnLocalNetwork(); _flushThread = null; })
                {
                    IsBackground = true
                };
                _flushThread.Start();
            }
        }

        private void KeepRivalListFlushToolStripMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                item.Checked = false;
                if (_flushThread != null)
                    _flushThread.Interrupt();
                _flushThread = null;
            }
            else
            {
                _flushThread = new Thread(() => { while (true) { FlushRivalsOnLocalNetwork(); Thread.Sleep(1000); } })
                {
                    IsBackground = true
                };
                _flushThread.Start();
                item.Checked = true;
            }
        }

        private void NetworkPrefixSettingToolStripMenuItemClick(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("当前遍历网络前缀：" + _networkPrefix, "请输入网络前缀", _networkPrefix);
            if (input.Length != 0)
                _networkPrefix = input;
        }

        private void UserNameSettingToolStripMenuItemClick(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("当前用户名：" + _userName, "请输入用户名", _userName);
            if (input.Length != 0)
                _userName = input;
        }

        private void TravelPortSettingToolStripMenuItemClick(object sender, EventArgs e)
        {
            string portsString = "";
            foreach (int port in _travelPorts)
                portsString += port.ToString() + " ";
            string input = Interaction.InputBox("当前遍历端口：" + portsString + "\n端口号应在1024~5000之间\n多个端口之间用空格分开", "请输入端口", portsString);

            if (input.Length != 0)
            {
                List<int> ports = new();
                foreach (var str in input.Split(' '))
                    if (str.Length > 0)
                        ports.Add(Int32.Parse(str));
                _travelPorts = ports;
            }
        }

        private void ListenPortSettingToolStripMenuItemClick(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("当前监听端口：" + ListenPort, "请输入端口", ListenPort.ToString());
            if (input.Length != 0)
                ListenPort = Int32.Parse(input);
        }

        private void WatchwordSettingToolStripMenuItemClick(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("当前口令：" + _watchword, "请输入口令", _watchword);
            if (input.Length != 0)
                _watchword = input;
        }
        private void WatchwordHintSettingToolStripMenuItemClick(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("当前口令提示：" + _watchwordHint, "请输入口令提示", _watchwordHint);
            if (input.Length != 0)
                _watchwordHint = input;
        }

        public void FlushRivalsOnLocalNetwork()
        {
            for (int i = 1; i <= 255; i++)
            {
                Ping ping = new();
                ping.PingCompleted += new PingCompletedEventHandler(PingCompleted);

                string pingIP = _networkPrefix + i.ToString();
                ping.SendAsync(pingIP, 2000, null);
            }
        }

        private void PingCompleted(object sender, PingCompletedEventArgs e)
        {
            string SelfName = Dns.GetHostName();
            IPAddress[] SelfIPs = Dns.GetHostAddresses(SelfName);
            IPAddress ip = e.Reply.Address;

            if (e.Reply.Status == IPStatus.Success)
            {
                foreach (var port in _travelPorts)
                {
                    if (Array.IndexOf(SelfIPs, ip) != -1 && port == ListenPort)
                        break;

                    Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    IPEndPoint ipEndPoint = new(ip, port);
                    try
                    {
                        socket.Connect(ipEndPoint);

                        Send(socket, "GetName");
                        //接收数据
                        string name = Receive(socket);

                        bool find = false;
                        foreach (ListViewItem item in listView.Items)
                        {
                            if (item.SubItems[1].Text == ip.ToString() && item.SubItems[2].Text == port.ToString())
                            {
                                item.SubItems[0].Text = name;
                                find = true;
                            }
                        }
                        if (!find)
                        {
                            ListViewItem viewItem = new ListViewItem(name);
                            viewItem.SubItems.Add(ipEndPoint.Address.ToString());
                            viewItem.SubItems.Add(port.ToString());
                            listView.Items.Add(viewItem);
                        }

                        Send(socket, "Close");
                        socket.Close();
                    }
                    catch { }
                }
            }
        }

        private void Listen() //建立与客户端的连接
        {
            try
            {
                while (true)
                {
                    //4.阻塞到有client连接，得到用于socket的连接
                    Socket socketAccept = _listenSocket.Accept();
                    if (_communicateSocket != null)
                        Send(socketAccept, "Busy");

                    string command = Receive(socketAccept);
                    string[] strs = command.Split(' ');
                    switch (strs[0])
                    {
                        case "GetName":
                            Send(socketAccept, _userName);
                            socketAccept.Close();
                            break;
                        case "Connect":
                            if (_watchwordHint.Length > 0 && _watchword.Length > 0)
                            {
                                Send(socketAccept, "WatchwordHint " + _watchwordHint);
                                if (Receive(socketAccept) == _watchword)
                                {
                                    Send(socketAccept, "Accept");
                                    toolStripStatusLabelLeft.Text = strs[1] + " connected";
                                    _communicateSocket = socketAccept;
                                    _communicateThread = new Thread(Communicate)
                                    {
                                        IsBackground = true    //运行线程在后台执行
                                    };   //线程绑定Listen函数
                                    _communicateThread.Start();
                                    MessageBox.Show("收到了来自" + strs[1] + "的连接请求，对方通过了口令验证并连接成功");
                                }
                                else
                                {
                                    Send(socketAccept, "Reject");
                                    socketAccept.Close();
                                    return;
                                }
                            }
                            else
                            {
                                Send(socketAccept, "Accept");
                                toolStripStatusLabelLeft.Text = strs[1] + " connected";
                                _communicateSocket = socketAccept;
                                _communicateThread = new Thread(Communicate)
                                {
                                    IsBackground = true    //运行线程在后台执行
                                };   //线程绑定Listen函数
                                _communicateThread.Start();
                                MessageBox.Show("收到了来自" + strs[1] + "的连接请求，已自动连接成功");
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
            catch (ThreadInterruptedException) { }
            catch (SocketException) { }
        }

        private void Communicate() //接收客户端数据
        {
            try
            {
                while (true)
                {
                    string command = Receive(_communicateSocket);
                    string[] strs = command.Split(' ');

                    switch (strs[0])
                    {
                        case "StartGame":
                            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                            DialogResult dr = MessageBox.Show("对方发起了游戏请求，是否接受", "开始游戏", messButton);
                            if (dr == DialogResult.OK)
                            {
                                Send(_communicateSocket, "GameAccept");
                                StartGame();
                            }
                            else
                                Send(_communicateSocket, "GameReject");
                            break;
                        case "GameAccept":
                            StartGame();
                            MainButtun.Enabled = true;
                            break;
                        case "GameReject":
                            MessageBox.Show("对方拒绝开始游戏");
                            MainButtun.Enabled = true;
                            TurnIdle();
                            break;
                        case "FinishPrepare":
                            statusStrip.Items[2].Text = "对方已完成了准备";
                            while (state != GameState.gaming) ;
                            statusStrip.Items[2].Text = "游戏中";
                            MainButtun.Enabled = true;
                            MainButtun.Text = "轰炸";
                            break;
                        case "GetBombResult":
                            if (state == GameState.idle)
                                break;

                            int Y = strs[1][0] - 'A';
                            int X = strs[1][1] - '0';
                            BombResult result = gridNetworkOurSide.BombAndDraw(X, Y);
                            Send(_communicateSocket, "BombResult " + strs[1] + " " + result.ToString());
                            if (state == GameState.wait)
                            {
                                state = GameState.gaming;
                                statusStrip.Items[2].Text = "游戏中";
                                MainButtun.Enabled = true;
                            }
                            else
                            {
                                statusStrip.Items[2].Text = "对方已完成了轰炸";
                                state = GameState.wait;
                            }
                            break;
                        case "BombResult":
                            BombResult bombResult = (BombResult)Enum.Parse(typeof(BombResult), strs[2], true);
                            Point point = new(strs[1][1] - '0', strs[1][0] - 'A');
                            gridNetworkCounterSide.DrawBombResult(point, bombResult);

                            if (bombResult == BombResult.head)
                                _numOfHitPlane++;

                            if (_numOfHitPlane == GridNetwork.NumOfPlane)
                            {
                                Send(_communicateSocket, "GameOver");
                                TurnIdle();
                                MessageBox.Show("你获得了胜利");
                            }
                            else
                            {
                                if (state == GameState.wait)
                                {
                                    state = GameState.gaming;
                                    statusStrip.Items[2].Text = "游戏中";
                                }
                                else
                                {
                                    state = GameState.wait;
                                    statusStrip.Items[2].Text = "等待对手完成选择";
                                    MainButtun.Enabled = false;
                                }
                            }
                            break;
                        case "GameOver":
                            TurnIdle();
                            MessageBox.Show("你失败了");
                            break;
                        case "Close":
                            _communicateSocket.Close();
                            _communicateSocket = null;
                            return;
                        default:
                            Send(_communicateSocket, "Unexpected command");
                            break;
                    }
                }
            }
            catch (ThreadInterruptedException)
            {
                _communicateSocket.Close();
                _communicateSocket = null;
            }
        }

        private static string Receive(Socket socket) //接收客户端数据
        {
            byte[] buffer = new byte[1024];
            int len = socket.Receive(buffer);
            return Encoding.Unicode.GetString(buffer, 0, len);
        }

        private static void Send(Socket socket, string text) // 向客户端发送数据
        {
            //SFlag标志连接成功，同时当客户端是打开的时候才能发送数据
            byte[] buffer = Encoding.Unicode.GetBytes(text);    //将字符串转成字节格式发送
            socket.Send(buffer);  //调用Send()向客户端发送数据
        }

        private GameState state = GameState.idle;
        private int _numOfHitPlane = 0;

        private string _networkPrefix = "192.168.2.";
        private Thread? _flushThread;
        private List<int> _travelPorts = new List<int>(new int[] { 2001 });

        private int _port = -1;
        private int ListenPort
        {
            get { return _port; }
            set
            {
                try
                {
                    if (_listenThread != null)
                        _listenThread.Interrupt();

                    if (_listenSocket != null)
                        _listenSocket.Close();
                    _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    _listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, -300);

                    //1.绑定IP和Port
                    IPEndPoint iPEndPoint = new(IPAddress.Any, value);
                    //2.使用Bind()进行绑定
                    _listenSocket.Bind(iPEndPoint);
                    //3.开启监听
                    _listenSocket.Listen(256); //Listen(int backlog); backlog:监听数量 

                    //开启线程Accept进行通信的客户端socket
                    _listenThread = new Thread(Listen)
                    {
                        IsBackground = true    //运行线程在后台执行
                    };   //线程绑定Listen函数
                    _listenThread.Start();    //Start里面的参数是Listen函数所需要的参数，这里传送的是用于通信的Socket对象

                    _port = value;
                }
                catch (SocketException)
                {
                    MessageBox.Show("监听程序启动失败，请尝试重新设置监听端口");
                }
            }
        }
        private string _userName = "Default";
        private string _watchword = "小鸡炖蘑菇";
        private string _watchwordHint = "天王盖地虎";

        private Socket _listenSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);    //声明用于监听的套接字;
        private Thread? _listenThread;     //声明线程

        private Socket? _communicateSocket; //负责通信的socket
        private Thread? _communicateThread;


        private AI ai;
    }

    public enum GameState
    {
        idle,
        preparing,
        gaming,
        wait,
    };

    public enum BombResult
    {
        body,
        head,
        none,
        error,
    }
}
