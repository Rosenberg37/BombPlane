using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Numerics;

namespace BombPlane
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            ListRivals();
        }

        private void MainButtunClick(object sender, EventArgs e)
        {
            switch (state)
            {
                case State.idle:
                    state = State.preparing;
                    gridNetworkOurSide.PlaneVisiblibity = true;
                    MainButtun.Text = "确认飞机位置";
                    break;
                case State.preparing:
                    state = State.gaming;
                    MainButtun.Text = "轰炸";
                    break;
                case State.gaming:
                    Point bombPoint = _selectedRival.GetBombPoint();
                    if (gridNetworkOurSide.Bomb(bombPoint) == BombResult.head)
                        _numOfHittedPlane++;

                    Point selectPoint = gridNetworkCounterSide.SelectedButtonPoint;
                    BombResult result = _selectedRival.Bomb(selectPoint);
                    gridNetworkCounterSide.DrawBombResult(selectPoint, result);
                    if (result == BombResult.head)
                        _numOfHitPlane++;


                    if (_numOfHitPlane == GridNetwork.NumOfPlane)
                    {
                        state = State.idle;
                        MainButtun.Text = "开始游戏";
                        gridNetworkOurSide.PlaneVisiblibity = false;
                        gridNetworkCounterSide.Clear();
                        gridNetworkOurSide.Clear();
                        _numOfHitPlane = 0;
                        _numOfHittedPlane = 0;
                    }
                    if (_numOfHittedPlane == GridNetwork.NumOfPlane)
                    {
                        state = State.idle;
                        MainButtun.Text = "开始游戏";
                        gridNetworkOurSide.PlaneVisiblibity = false;
                        gridNetworkCounterSide.Clear();
                        gridNetworkOurSide.Clear();
                        _numOfHittedPlane = 0;
                    }
                    break;
            }
        }


        public void ListRivals()
        {
            ListViewItem item = new ListViewItem("AI");
            item.SubItems.Add("Local");
            listView.Items.Add(item);

            //Thread enumrateThread = new Thread(new ThreadStart(ListValidIPs));
            //enumrateThread.IsBackground = true;
            //enumrateThread.Start(this);
        }

        private void ListValidIPs()
        {
            while (true)
            {
                for (int i = 1; i <= 255; i++)
                {
                    Ping ping;
                    ping = new Ping();
                    ping.PingCompleted += new PingCompletedEventHandler(PingCompleted);

                    string pingIP = "192.168.1." + i.ToString();
                    ping.SendAsync(pingIP, 2000, null);
                }
            }
        }

        private void PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply.Status != IPStatus.Success)
            {

            }
            else
            {
                IPAddress IP = e.Reply.Address;
                IPEndPoint ipEndPoint = new IPEndPoint(IP, ConnectPort);
                try
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(ipEndPoint);
                    string name = ReceiveName(socket);
                    ListViewItem item = new ListViewItem(name);
                    item.SubItems.Add(ipEndPoint.Address.ToString());
                    listView.Items.Add(item);
                    socket.Close();
                }
                catch { }
            }
        }

        private static string ReceiveName(Socket socket)
        {
            //接收数据
            byte[] receive = new byte[1024];
            socket.Receive(receive);  //调用Receive()接收字节数据
            if (receive.Length > 0)
                return Encoding.ASCII.GetString(receive);  //将字节数据根据ASCII码转成字符串
            else
                throw new DataException("Unexpected null name");
        }


        private List<Rival> RivalList
        {
            get
            {
                List<Rival> rivals = new List<Rival>();
                foreach (ListViewItem item in listView.Items)
                {
                    if (item.SubItems[0].Text == "AI")
                    {
                        rivals.Add(new Rival());
                    }
                    else { }
                }
                return rivals;
            }
        }
        public const int ConnectPort = 12345;

        internal enum State
        {
            idle,
            preparing,
            gaming,
        };

        private State state = State.idle;

        private int _numOfHitPlane;
        private int _numOfHittedPlane;
        private Rival _selectedRival;

        private void ConnectToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 1)
            {
                if (listView.SelectedItems[0].SubItems[0].Text == "AI")
                {
                    toolStripStatusLabelLeft.Text = listView.SelectedItems[0].SubItems[1].Text + " Connect";
                    _selectedRival = new Rival();
                }
            }
        }
    }


    public class Rival
    {
        public Rival()
        {
            _planes = new Plane[GridNetwork.NumOfPlane] {
                new Plane(Direction.Up, 2, 0, null),
                new Plane(Direction.Up, 2, 6, null),
                new Plane(Direction.Up, 7, 2, null)
            };
        }


        public Point GetBombPoint()
        {
            Random random = new Random();
            int X = random.Next(0, 9);
            int Y = random.Next(0, 9);
            return new Point(X, Y);
        }

        public BombResult Bomb(Point point)
        {
            foreach (Plane plane in _planes)
                if (point.X == plane.HeadX && point.Y == plane.HeadY)
                    return BombResult.head;
                else
                    foreach (Point planePoint in plane.BodyPoints)
                        if (point.X == planePoint.X && point.Y == planePoint.Y)
                            return BombResult.body;
            return BombResult.none;
        }


        public string Name { get { return "AI"; } }
        public string IP { get { return "Local"; } }
        internal Plane[] _planes;
    }
}
