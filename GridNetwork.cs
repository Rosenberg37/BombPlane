using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombPlane
{
    public partial class GridNetwork : UserControl
    {
        public GridNetwork()
        {
            InitializeComponent();
            _planes = new Plane[NumOfPlane] {
                new Plane(Direction.Up, 2, 0, this),
                new Plane(Direction.Up, 2, 6, this),
                new Plane(Direction.Up, 7, 2, this)
            };
            foreach (Control control in tableLayoutPanel.Controls)
                control.MouseClick += new MouseEventHandler(ButtonMouseClickForSelectButton);

        }

        public Control GetControlFromPosition(int X, int Y)
        {
            return tableLayoutPanel.GetControlFromPosition(X, Y);
        }

        public Control GetControlFromPosition(Point point)
        {
            return tableLayoutPanel.GetControlFromPosition(point.X, point.Y);
        }

        public void Clear()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                control.BackColor = SystemColors.ControlDark;
                control.ForeColor = SystemColors.ControlText;
                int X = tableLayoutPanel.GetColumn(control);
                int Y = tableLayoutPanel.GetRow(control);
                String column = X.ToString();
                String row = ((char)('A' + Y)).ToString();
                control.Text = row + column;
            }

        }



        public BombResult Bomb(Point point)
        {
            Button selectedButton = (Button)GetControlFromPosition(point);
            if (selectedButton.BackColor != Color.Yellow)
            {
                selectedButton.BackColor = Color.Yellow;
                foreach (Plane plane in _planes)
                {
                    if (point.X == plane.HeadX && point.Y == plane.HeadY)
                    {
                        DrawBombResult(point, BombResult.head);
                        return BombResult.head;
                    }
                    else
                    {
                        foreach (Point planePoint in plane.BodyPoints)
                        {
                            if (point.X == planePoint.X && point.Y == planePoint.Y)
                            {
                                DrawBombResult(point, BombResult.body);
                                return BombResult.body;
                            }
                        }
                    }
                }
                DrawBombResult(point, BombResult.none);
                return BombResult.none;
            }
            return BombResult.error;
        }

        public void DrawBombResult(Point point, BombResult result)
        {
            Button selectedButton = (Button)GetControlFromPosition(point);
            if (result != BombResult.error)
            {
                selectedButton.BackColor = Color.Yellow;
                switch (result)
                {
                    case BombResult.head:
                        selectedButton.Text = "头";
                        selectedButton.ForeColor = Color.OrangeRed;
                        break;
                    case BombResult.body:
                        selectedButton.Text = "身";
                        selectedButton.ForeColor = Color.LimeGreen;
                        break;
                    case BombResult.none:
                        selectedButton.Text = "空";
                        break;
                }
            }
        }

        private void ButtonMouseClickForSelectPlane(object sender, MouseEventArgs e)
        {
            int x = tableLayoutPanel.GetColumn(_selectedButton);
            int y = tableLayoutPanel.GetRow(_selectedButton);
            Point point = new Point(x, y);
            foreach (Plane plane in _planes)
            {
                if (plane.BodyPoints.Contains(point))
                {
                    _selectedPlane = plane;
                    return;
                }
            }
            _selectedPlane = null;
        }

        private void ButtonMouseClickForSelectButton(object sender, MouseEventArgs e)
        {
            _selectedButton = (Button)sender;
        }

        private void ButtonKeyPressForMove(object sender, KeyPressEventArgs e)
        {
            if (_selectedPlane != null)
            {
                switch (e.KeyChar)
                {
                    case 'w':
                        _selectedPlane.Move(Direction.Up);
                        break;
                    case 's':
                        _selectedPlane.Move(Direction.Down);
                        break;
                    case 'a':
                        _selectedPlane.Move(Direction.Left);
                        break;
                    case 'd':
                        _selectedPlane.Move(Direction.Right);
                        break;
                    case 'r':
                        _selectedPlane.Rotate();
                        break;
                }
            }
        }


        public int RowCount { get { return tableLayoutPanel.RowCount; } }
        public int ColumnCount { get { return tableLayoutPanel.ColumnCount; } }

        private bool _planeVisible;
        public bool PlaneVisiblibity
        {
            get
            {
                return _planeVisible;
            }
            set
            {
                _planeVisible = value;

                if (value)
                {
                    foreach (Plane plane in _planes)
                        plane.Draw();
                    foreach (Control control in this.tableLayoutPanel.Controls)
                    {
                        control.MouseClick += new MouseEventHandler(ButtonMouseClickForSelectPlane);
                        control.KeyPress += new KeyPressEventHandler(ButtonKeyPressForMove);
                    }
                }
                else
                {
                    foreach (Plane plane in _planes)
                        plane.Clear();
                    foreach (Control control in this.tableLayoutPanel.Controls)
                    {
                        control.MouseClick -= new MouseEventHandler(ButtonMouseClickForSelectPlane);
                        control.KeyPress -= new KeyPressEventHandler(ButtonKeyPressForMove);
                    }
                }
            }
        }

        internal Plane[] _planes;
        internal Plane? _selectedPlane;
        private Button _selectedButton;
        public int SelectedButtonX { get { return tableLayoutPanel.GetColumn(_selectedButton); } }
        public int SelectedButtonY { get { return tableLayoutPanel.GetRow(_selectedButton); } }
        public Point SelectedButtonPoint { get { return new Point(SelectedButtonX, SelectedButtonY); } }
        public const int NumOfPlane = 3;
    }


    internal class Plane
    {
        public Plane(Direction direction, int headX, int headY, GridNetwork? gridNetwork)
        {
            this.direction = direction;
            this.gridNetwork = gridNetwork;
            HeadPoint = new Point(headX, headY);
        }


        internal bool Move(Direction direction)
        {
            bool valid = true;
            Point origin = HeadPoint;
            Clear();
            switch (direction)
            {
                case Direction.Up:
                    HeadY -= 1;
                    break;
                case Direction.Down:
                    HeadY += 1;
                    break;
                case Direction.Left:
                    HeadX -= 1;
                    break;
                case Direction.Right:
                    HeadX += 1;
                    break;
            }
            if (!CheckPlaneValid())
            {
                HeadPoint = origin;
                valid = false;
            }
            Draw();
            return valid;
        }

        internal bool Rotate()
        {
            bool valid = true;
            Direction origin = direction;
            Clear();
            switch (direction)
            {
                case Direction.Up:
                    direction = Direction.Right;
                    break;
                case Direction.Down:
                    direction = Direction.Left;
                    break;
                case Direction.Left:
                    direction = Direction.Up;
                    break;
                case Direction.Right:
                    direction = Direction.Down;
                    break;
            }
            if (!CheckPlaneValid())
            {
                direction = origin;
                valid = false;
            }
            Draw();
            return valid;
        }

        private bool CheckPlaneValid()
        {
            foreach (var point in BodyPoints)
                if (point.X < 0 || point.Y < 0 || point.X >= gridNetwork.ColumnCount || point.Y >= gridNetwork.RowCount)
                    return false;
            foreach (Plane plane in gridNetwork._planes)
                if (!ReferenceEquals(plane, this) && plane.BodyPoints.Intersect(BodyPoints).Count() > 0)
                    return false;
            return true;
        }
        internal void Clear()
        {
            foreach (var point in BodyPoints)
            {
                Control control = gridNetwork.GetControlFromPosition(point.X, point.Y);
                control.BackColor = SystemColors.ControlDark;
            }
        }


        internal void Draw()
        {
            foreach (var point in BodyPoints)
            {
                Control control = gridNetwork.GetControlFromPosition(point.X, point.Y);
                control.BackColor = Color.LimeGreen;
            }
            Control headControl = gridNetwork.GetControlFromPosition(HeadPoint.X, HeadPoint.Y);
            headControl.BackColor = Color.OrangeRed;
        }

        public Direction direction;
        public Point HeadPoint;
        private GridNetwork gridNetwork;

        public int HeadX { get { return HeadPoint.X; } set { HeadPoint.X = value; } }
        public int HeadY { get { return HeadPoint.Y; } set { HeadPoint.Y = value; } }


        public HashSet<Point> BodyPoints
        {
            get
            {
                Point[] points;
                switch (direction)
                {
                    case Direction.Up:
                        points = new Point[10]
                        {
                            new Point(HeadX, HeadY),
                            new Point(HeadX, HeadY + 1),
                            new Point(HeadX - 1, HeadY + 1),
                            new Point(HeadX + 1, HeadY + 1),
                            new Point(HeadX - 2, HeadY + 1),
                            new Point(HeadX + 2, HeadY + 1),
                            new Point(HeadX, HeadY + 2),
                            new Point(HeadX, HeadY + 3),
                            new Point(HeadX + 1, HeadY + 3),
                            new Point(HeadX - 1, HeadY + 3),
                        };
                        break;
                    case Direction.Down:
                        points = new Point[10]
                        {
                            new Point(HeadX, HeadY),
                            new Point(HeadX, HeadY - 1),
                            new Point(HeadX - 1, HeadY - 1),
                            new Point(HeadX + 1, HeadY - 1),
                            new Point(HeadX - 2, HeadY - 1),
                            new Point(HeadX + 2, HeadY - 1),
                            new Point(HeadX, HeadY - 2),
                            new Point(HeadX, HeadY - 3),
                            new Point(HeadX + 1, HeadY - 3),
                            new Point(HeadX - 1, HeadY - 3),
                        };
                        break;
                    case Direction.Left:
                        points = new Point[10]
                        {
                            new Point(HeadX, HeadY),
                            new Point(HeadX + 1, HeadY),
                            new Point(HeadX + 1, HeadY + 1),
                            new Point(HeadX + 1, HeadY - 1),
                            new Point(HeadX + 1, HeadY + 2),
                            new Point(HeadX + 1, HeadY - 2),
                            new Point(HeadX + 2, HeadY),
                            new Point(HeadX + 3, HeadY),
                            new Point(HeadX + 3, HeadY + 1),
                            new Point(HeadX + 3, HeadY - 1),
                        };
                        break;
                    case Direction.Right:
                        points = new Point[10]
                        {
                            new Point(HeadX, HeadY),
                            new Point(HeadX - 1, HeadY),
                            new Point(HeadX - 1, HeadY + 1),
                            new Point(HeadX - 1, HeadY - 1),
                            new Point(HeadX - 1, HeadY + 2),
                            new Point(HeadX - 1, HeadY - 2),
                            new Point(HeadX - 2, HeadY),
                            new Point(HeadX - 3, HeadY),
                            new Point(HeadX - 3, HeadY + 1),
                            new Point(HeadX - 3, HeadY - 1),
                        };
                        break;
                    default:
                        points = new Point[10];
                        break;
                }
                return new HashSet<Point>(points);
            }
        }

    }
    public enum BombResult
    {
        body,
        head,
        none,
        error,
    }
    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}