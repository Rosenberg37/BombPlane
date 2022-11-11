using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using System.Numerics;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace BombPlane
{
    public partial class BombPLaneForm : Form
    {
        public BombPLaneForm()
        {
            InitializeComponent();

            // Let threads able to control the MainFrom controls;
            CheckForIllegalCrossThreadCalls = false;

            _listenCTS = new CancellationTokenSource();
            _listenCTS.Token.Register(() =>
            {
                _listenThread = null;
                _listenSocket.Close();
                _listenSocket = null;
            });

            _flushCTS = new CancellationTokenSource();
            _flushCTS.Token.Register(() =>
            {
                _flushThread = null;
            });

            _delegateCTS = new CancellationTokenSource();
            _delegateCTS.Token.Register(() =>
            {
                _delegateThread = null;
            });

            ListenPort = 2000;
            _travelPorts.Add(2001);
            _networkPrefix = "192.168.6";

            _ai = new AI(2001);
            _ai.Start();

            FlushRivalsOnLocalNetwork();
        }

        private GameState state = GameState.idle;
        private int _numOfHitPlane = 0;

        private Thread? _flushThread;
        CancellationTokenSource? _flushCTS;

        private string _userName = "Default";
        private string _watchword = "小鸡炖蘑菇";
        private string _watchwordHint = "天王盖地虎";

        private string _networkPrefix;
        private List<int> _travelPorts = new();

        private int _listenPort = -1;
        private int ListenPort
        {
            get { return _listenPort; }
            set
            {
                try
                {
                    if (_listenThread != null)
                        _listenCTS.Cancel();

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
                    _listenThread.Start(_listenCTS.Token);    //Start里面的参数是Listen函数所需要的参数

                    _listenPort = value;
                }
                catch (SocketException)
                {
                    MessageBox.Show("监听程序启动失败，请尝试重新设置监听端口");
                }
            }
        }
        private Socket _listenSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);    //声明用于监听的套接字;
        private Thread? _listenThread;     //声明线程
        private CancellationTokenSource? _listenCTS;

        private Socket? _communicateSocket; //负责通信的socket
        private Thread? _communicateThread;

        private AI _ai;

        private string? _delegateProgram;
        private Thread? _delegateThread;     //声明线程
        private CancellationTokenSource? _delegateCTS;


        private int _count = 0;


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

        private void ConnectGame(string RivalName, Socket socket)
        {
            toolStripStatusLabelLeft.Text = RivalName + " connected";
            if (_communicateSocket != null)
                _communicateSocket.Close();
            _communicateSocket = socket;
            _communicateThread = new Thread(Communicate)
            {
                IsBackground = true
            };
            _communicateThread.Start();
            MainButton.Enabled = true;
            StartGameToolStripMenuItem.Enabled = true;
            DisconnectToolStripMenuItem.Enabled = true;
        }

        private void StartGame()
        {
            if (_communicateSocket == null)
                MessageBox.Show("请先选择对手");
            else
            {
                Send(_communicateSocket, "StartGame");
                statusStrip.Items[2].Text = "等待对方回应";
                MainButton.Enabled = false;
                StartGameToolStripMenuItem.Enabled = false;
                FinishPrepareToolStripMenuItem.Enabled = true;
                InitializePlanesToolStripMenuItem.Enabled = true;
            }
        }

        private void FinishPrepare()
        {
            if (!gridNetworkOurSide.CheckPlanesValid())
                MessageBox.Show("当前飞机位置不合法，请避免飞机重叠");
            else
            {
                Send(_communicateSocket, "FinishPrepare");
                statusStrip.Items[2].Text = "等待对手完成准备";
                MainButton.Enabled = false;
                FinishPrepareToolStripMenuItem.Enabled = false;
                InitializePlanesToolStripMenuItem.Enabled = false;
                state = GameState.gaming;
            }
        }

        private void TurnPrepare()
        {
            state = GameState.preparing;
            statusStrip.Items[2].Text = "准备中";
            gridNetworkOurSide.InitializePlanes();
            gridNetworkOurSide.IsPlaneVisible = true;
            MainButton.Text = "完成准备";
            MainButton.Enabled = true;
        }

        private void TurnIdle()
        {
            state = GameState.idle;
            statusStrip.Items[2].Text = "闲置";
            MainButton.Text = "开始游戏";
            MainButton.Enabled = true;
            gridNetworkOurSide.IsPlaneVisible = false;
            gridNetworkOurSide.ClearGrids();
            gridNetworkCounterSide.ClearGrids();
            _numOfHitPlane = 0;
            StartGameToolStripMenuItem.Enabled = true;
            BombToolStripMenuItem.Enabled = false;
            AssistToolStripMenuItem.Enabled = false;
            InitializePlanesToolStripMenuItem.Enabled = false;
            FinishPrepareToolStripMenuItem.Enabled = false;
        }

        private void TurnGame()
        {
            state = GameState.gaming;
            statusStrip.Items[2].Text = "游戏中";
            MainButton.Enabled = true;
            BombToolStripMenuItem.Enabled = true;
            AssistToolStripMenuItem.Enabled = true;
            MainButton.Text = "轰炸";
        }

        private void Bomb(Point? point)
        {
            try
            {
                if (point == null)
                    MessageBox.Show("请先点击选择轰炸的点");
                else
                {
                    int? Y = gridNetworkCounterSide.SelectedButtonY;
                    int? X = gridNetworkCounterSide.SelectedButtonX;
                    string pos = GridView.ConvertPointToString((int)X, (int)Y);
                    Send(_communicateSocket, string.Format("GetBombResult {0}", pos));
                    MainButton.Enabled = false;
                    BombToolStripMenuItem.Enabled = false;
                    AssistToolStripMenuItem.Enabled = false;
                }
            }
            catch (DuplicatedSelectionException)
            {
                MessageBox.Show("选定轰炸位置不合理，请不要选择轰炸过的位置");
            }
        }

        private void Bomb() { Bomb(gridNetworkCounterSide.GetBombPoint()); }

        private void GameOver()
        {
            Send(_communicateSocket, "GameOver");
            TurnIdle();
            MessageBox.Show("你获得了胜利");
        }

        public void FlushRivalsOnLocalNetwork()
        {
            Thread thread = new(() =>
            {
                for (int i = listView.Items.Count - 1; i >= 0; i--)
                {
                    string ip = listView.Items[i].SubItems[1].Text;
                    Ping ping = new();
                    PingReply reply = ping.Send(ip);
                    if (reply.Status != IPStatus.Success)
                        listView.Items.RemoveAt(i);

                    int port = int.Parse(listView.Items[i].SubItems[2].Text);
                    if (!_travelPorts.Contains(port))
                        listView.Items.RemoveAt(i);
                }

                string SelfName = Dns.GetHostName();
                IPAddress[] SelfIPs = Dns.GetHostAddresses(SelfName);

                for (int i = 1; i <= 255; i++)
                {
                    Ping ping = new();
                    ping.PingCompleted += new PingCompletedEventHandler((sender, e) =>
                    {
                        string ip = e.Reply.Address.ToString();
                        if (e.Reply.Status == IPStatus.Success)
                        {
                            foreach (int port in _travelPorts)
                            {
                                if (Array.IndexOf(SelfIPs, ip) != -1 && port == ListenPort)
                                    break;

                                Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                                IPEndPoint ipEndPoint = new(IPAddress.Parse(ip), port);
                                try
                                {
                                    socket.Connect(ipEndPoint);

                                    Send(socket, "GetName");
                                    //接收数据
                                    string name = Receive(socket);

                                    bool find = false;
                                    foreach (ListViewItem item in listView.Items)
                                    {
                                        if (item.SubItems[1].Text == ip && item.SubItems[2].Text == port.ToString())
                                        {
                                            item.SubItems[0].Text = name;
                                            find = true;
                                        }
                                    }
                                    if (!find)
                                    {
                                        ListViewItem viewItem = new(name);
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
                    });
                    ping.SendAsync(_networkPrefix + "." + i.ToString(), null);
                }
            })
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void Listen(object obj) //建立与客户端的连接
        {
            CancellationToken ct = (CancellationToken)obj;

            while (!ct.IsCancellationRequested)
            {
                try
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
                                    string name = strs[1];
                                    ConnectGame(name, socketAccept);
                                    MessageBox.Show("收到了来自" + name + "的连接请求，对方通过了口令验证并连接成功");
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
                                string name = strs[1];
                                ConnectGame(name, socketAccept);
                                MessageBox.Show("收到了来自" + name + "的连接请求，已自动连接成功");
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (SocketException) { }
            }
        }

        private void Communicate()
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
                                TurnPrepare();
                            }
                            else
                                Send(_communicateSocket, "GameReject");
                            break;
                        case "GameAccept":
                            TurnPrepare();
                            break;
                        case "GameReject":
                            MessageBox.Show("对方拒绝开始游戏");
                            TurnIdle();
                            break;
                        case "FinishPrepare":
                            statusStrip.Items[2].Text = "对方已完成了准备";
                            while (state != GameState.gaming) ;
                            TurnGame();
                            break;
                        case "GetBombResult":
                            if (state == GameState.idle)
                                break;

                            Point bombPoint = GridView.ConvertStringToPoint(strs[1]);
                            BombResult result = gridNetworkOurSide.BombAndDraw(bombPoint);
                            Send(_communicateSocket, "BombResult " + strs[1] + " " + result.ToString());
                            if (state == GameState.wait)
                                TurnGame();
                            else
                            {
                                statusStrip.Items[2].Text = "对方已完成了轰炸";
                                state = GameState.wait;
                            }
                            break;
                        case "BombResult":
                            BombResult bombResult = (BombResult)Enum.Parse(typeof(BombResult), strs[2], true);
                            Point drawPoint = GridView.ConvertStringToPoint(strs[1]);
                            gridNetworkCounterSide.DrawBombResult(drawPoint, bombResult);

                            if (bombResult == BombResult.head)
                                _numOfHitPlane++;

                            if (_numOfHitPlane == GridView.NumOfPlane)
                                GameOver();
                            else
                            {
                                if (state == GameState.wait)
                                    TurnGame();
                                else
                                {
                                    state = GameState.wait;
                                    statusStrip.Items[2].Text = "等待对手完成选择";
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
            catch (ReceiveExpection) { }
        }

        private void MainButtunClick(object sender, EventArgs e)
        {
            switch (state)
            {
                case GameState.idle:
                    StartGame();
                    break;
                case GameState.preparing:
                    FinishPrepare();
                    break;
                case GameState.gaming:
                case GameState.wait:
                    Bomb();
                    break;
            }
        }

        private void FlushToolStripMenuItemClick(object sender, EventArgs e)
        {
            FlushRivalListToolStripMenuItemClick(sender, e);
        }

        private void ConnectToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                MessageBox.Show("请选择一个对手后尝试连接");
            else
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
                    //设置请求超时
                    clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, -300);
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
                                ConnectGame(name, clientSocket);
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
                        ConnectGame(name, clientSocket);
                    else
                        MessageBox.Show("连接失败了");
                }
                catch (SocketException)
                {
                    MessageBox.Show("无法连接目标客户端");
                }
            }

        }

        private void FlushRivalListToolStripMenuItemClick(object sender, EventArgs e)
        {
            FlushRivalsOnLocalNetwork();
        }

        private void KeepRivalListFlushToolStripMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                item.Checked = false;
                if (_flushThread != null)
                    _flushCTS.Cancel();
            }
            else
            {
                _flushThread = new Thread((obj) =>
                {
                    CancellationToken ct = (CancellationToken)obj;
                    while (ct.IsCancellationRequested)
                    {
                        FlushRivalsOnLocalNetwork();
                        Thread.Sleep(1000);
                    }
                })
                {
                    IsBackground = true
                };
                _flushThread.Start(_flushCTS.Token);
                item.Checked = true;
            }
        }

        private void NetworkPrefixSettingToolStripMenuItemClick(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("当前遍历网络前缀：" + _networkPrefix, "请输入网络前缀", _networkPrefix);
            if (input.Length != 0)
            {
                string[] strs = input.Split('.');
                if (strs.Length != 3)
                    MessageBox.Show("意料之外的前缀输入，请确保输入ip前缀的前三段");
                else
                {
                    try
                    {
                        foreach (string str in strs)
                        {
                            int num = int.Parse(str);
                            if (num < 0 || num > 255)
                                throw new ArgumentException();
                        }
                        _networkPrefix = input;
                        FlushRivalsOnLocalNetwork();
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("IP地址格式不正确");
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("IP地址范围不正确");
                    }
                }
            }
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
            string input = Interaction.InputBox("当前遍历端口：" + portsString + "\n端口号应在1024~65535之间\n多个端口之间用空格分开", "请输入端口", portsString);

            if (input.Length != 0)
            {
                List<int> ports = new();
                List<string> invalidStrs = new();
                List<int> invalidPorts = new();
                foreach (var str in input.Split(' '))
                {
                    if (str.Length > 0)
                    {
                        try
                        {
                            int port = int.Parse(str);
                            if (port < 1024 || port > 65535)
                                invalidPorts.Add(port);
                            ports.Add(port);
                        }
                        catch (FormatException)
                        {
                            invalidStrs.Add(str);
                        }
                    }
                }
                _travelPorts = ports;

                StringBuilder errorMessage = new();
                if (invalidStrs.Count > 0)
                {
                    errorMessage.Append("已过滤无效格式端口：");
                    foreach (string str in invalidStrs)
                        errorMessage.Append(str + " ");
                    errorMessage.Append("\n");
                }
                if (invalidPorts.Count > 0)
                {
                    errorMessage.Append("已过滤无效端口号：");
                    foreach (string str in invalidStrs)
                        errorMessage.Append(str + " ");
                    errorMessage.Append("\n");
                }
                if (errorMessage.Length > 0)
                    MessageBox.Show(errorMessage.ToString());
                else
                    FlushRivalsOnLocalNetwork();
            }
        }

        private void ListenPortSettingToolStripMenuItemClick(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("当前监听端口：" + ListenPort, "请输入端口", ListenPort.ToString());
            if (input.Length != 0)
                ListenPort = int.Parse(input);
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

        private void UserNameHelpToolStripMenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show("当前用户名：" + _userName + "\n用户名可以在设置选项中设置，影响其他客户端扫描时显示的用户名");
        }

        private void PortAndNetworkHelpToolStripMenuItemClick(object sender, EventArgs e)
        {
            StringBuilder help = new("以下为端口设置和网络设置的一些说明\n");
            help.Append("游戏网络模块基于Ping和Socket来扫描某个网段上可连接的游戏程序\n");
            help.Append("其首先检测能否Ping通某个IP地址，然后尝试通过Socket连接并请求对方用户名\n");
            help.Append("若目标程序回应了用户名，则认为对方是游戏程序并添加到待选列表\n\n");
            help.Append("网络前缀：设置扫描的网段，暂只支持遍历最后一节，主要用于局域网\n");
            help.Append("遍历端口：设置扫描某个IP时尝试连接的端口，当某个游戏程序的监听端口正好包含在遍历端口集时，可以扫描到\n");
            help.Append("监听端口：设置游戏监听程序监听的端口号，当它包含在其他游戏程序的遍历端口时，可以被扫描到\n");
            help.Append("连接口令及提示：类似于房间密码，用于设置对方尝试连接看到的提示及正确的密码，设置为空时则不需要密码\n");
            MessageBox.Show(help.ToString());
        }

        private void GameProcedureHelpToolStripMenuItemClick(object sender, EventArgs e)
        {
            StringBuilder help = new("以下为游戏主要流程的简要说明\n");
            help.Append("在候选对手上右键点击“连接”后可以尝试连接对手\n");
            help.Append("连接成功后点击“开始游戏”则可以发送请求开始游戏请求\n");
            help.Append("准备阶段使用点击来选中飞机，并使用键盘“w”“s”“a”“d”“r”键来移动飞机，完成摆放后点击主按钮进入下阶段\n");
            help.Append("游戏阶段在对方棋盘上点击某个位置后点击轰炸既可以进行轰炸\n");
            help.Append("游戏结束时可以收到提示，窗口右下角会实时显示游戏状态\n");
            MessageBox.Show(help.ToString());
        }

        private void StartGameToolStripMenuItemClick(object sender, EventArgs e)
        {
            StartGame();
        }

        private void FinishPrepareToolStripMenuItemClick(object sender, EventArgs e)
        {
            FinishPrepare();
        }

        private void InitializePlanesToolStripMenuItemClick(object sender, EventArgs e)
        {
            gridNetworkOurSide.InitializePlanes();
        }

        private void BombToolStripMenuItemClick(object sender, EventArgs e) { Bomb(); }

        private void DisconnectToolStripMenuItemClick(object sender, EventArgs e)
        {
            DisconnectToolStripMenuItem.Enabled = false;
            toolStripStatusLabelLeft.Text = "Not Connected";
            _communicateSocket.Close();
            _communicateSocket = null;
            _communicateThread = null;
            TurnIdle();
        }

        private void AssistToolStripMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                item.Checked = false;
                gridNetworkCounterSide.IsPlaneVisible = false;
            }
            else
            {
                gridNetworkCounterSide.InitializePlanes();
                gridNetworkCounterSide.IsPlaneVisible = true;
                item.Checked = true;
            }
        }

        private void SetDelegateToolStripMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                item.Checked = false;
                _delegateProgram = null;
            }
            else
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;
                dialog.Title = "请选择代理程序";
                dialog.Filter = "所有文件|*.exe";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Process process = new();
                    process.StartInfo.FileName = dialog.FileName;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;

                    try
                    {
                        process.Start();
                        process.WaitForExit();
                        if (process.StandardOutput.ReadToEnd() == "BombPlane")
                            _delegateProgram = dialog.FileName;
                        else
                            MessageBox.Show("不是有效的代理程序");
                    }
                    catch { MessageBox.Show("无法启动代理程序"); }
                }
            }
        }

        private void DelegateInitializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new();
            process.StartInfo.FileName = _delegateProgram;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = "Initialize";

            try
            {
                process.Start();
                process.WaitForExit();
            }
            catch { MessageBox.Show("代理程序启动失败"); }

            string str = process.StandardOutput.ReadToEnd();
            string[] strs = str.Split(' ');
            List<Plane> planes = new();
            for (int i = 0; i < 3; i++)
            {
                Direction direction = (Direction)Enum.Parse(typeof(Direction), strs[i * 2], true);
                Point point = GridView.ConvertStringToPoint(strs[i * 2] + 1);
                planes.Add(new Plane(direction, point));
            }

            gridNetworkOurSide.Planes = planes.ToArray();
        }

        private void DelegateBombToolStripMenuItemClick(object sender, EventArgs e)
        {
            Process process = new();
            process.StartInfo.FileName = _delegateProgram;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            var results = gridNetworkCounterSide.GetCurrentGridStates();
            StringBuilder args = new();
            foreach (var result in results)
            {
                args.Append(GridView.ConvertPointToString(result.Item1));
                args.Append(' ');
                args.Append(result.Item2.ToString());
                args.Append(' ');
            }
            process.StartInfo.Arguments = args.ToString();

            try
            {
                process.Start();
                process.WaitForExit();
            }
            catch { MessageBox.Show("代理程序启动失败"); }

            string str = process.StandardOutput.ReadToEnd();
            Point point = GridView.ConvertStringToPoint(str);
            Bomb(point);
        }

        private void DelegateHostToolStripMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (item.Checked)
            {
                _delegateCTS.Cancel();
                item.Checked = false;
                MainButton.Enabled = true;
            }
            else
            {
                MainButton.Enabled = false;
                _delegateThread = new Thread((obj) =>
                {
                    CancellationToken ct = (CancellationToken)obj;
                    while (!ct.IsCancellationRequested)
                    {
                        switch (state)
                        {
                            case GameState.idle:
                                StartGame();
                                break;
                            case GameState.preparing:
                                FinishPrepare();
                                break;
                            case GameState.gaming:
                            case GameState.wait:
                                Process process = new();
                                process.StartInfo.FileName = _delegateProgram;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.RedirectStandardInput = true;
                                process.StartInfo.RedirectStandardOutput = true;
                                process.StartInfo.RedirectStandardError = true;
                                process.StartInfo.CreateNoWindow = true;

                                var results = gridNetworkCounterSide.GetCurrentGridStates();
                                StringBuilder args = new();
                                foreach (var result in results)
                                {
                                    args.Append(GridView.ConvertPointToString(result.Item1));
                                    args.Append(' ');
                                    args.Append(result.Item2.ToString());
                                    args.Append(' ');
                                }
                                process.StartInfo.Arguments = args.ToString();

                                try
                                {
                                    process.Start();
                                    process.WaitForExit();
                                }
                                catch { MessageBox.Show("代理程序启动失败"); }

                                string str = process.StandardOutput.ReadToEnd();
                                Point point = GridView.ConvertStringToPoint(str);
                                Bomb(point);
                                break;
                        }
                        Thread.Sleep(1000);
                    }
                });
                _delegateThread.Start(_delegateCTS.Token);
            }
        }

        private void PictureBoxDoubleClick(object sender, EventArgs e)
        {
            if (_count == -1)
            {
                string input = Interaction.InputBox("", "");
                switch (input)
                {
                    case "over":
                        GameOver();
                        break;
                }
            }
            else if (++_count == 5)
            {
                _count = 0;
                string input = Interaction.InputBox("", "");
                if (input == "shuniji")
                {
                    _count = -1;
                    Text += "*";
                }

            }
        }
    }

    public class ReceiveExpection : SocketException
    {
        public ReceiveExpection(SocketException e) : base(e.ErrorCode) { }
    }

    public class SendException : SocketException
    {
        public SendException(SocketException e) : base(e.ErrorCode) { }
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
