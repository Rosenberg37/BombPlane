using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BombPlane
{
    public partial class GridView : UserControl
    {
        public GridView()
        {
            InitializeComponent();
            foreach (Control control in tableLayoutPanel.Controls)
                control.MouseClick += new MouseEventHandler(ButtonMouseClickForSelect);
        }

        private Plane[]? _planes;
        public Plane[] Planes
        {
            get { return _planes; }
            set
            {
                if (value != null)
                {
                    if (value.Length != NumOfPlane)
                        throw new ArgumentException("Too many planes");
                    foreach (Plane plane in value)
                        if (!CheckPlaneBounded(plane))
                            throw new ArgumentException("Invalid plane");
                }
                _planes = value;
            }
        }
        private int _selectedPlaneIndex = -1;
        private Plane? SelectedPlane
        {
            get
            {
                if (_selectedPlaneIndex == -1)
                    return null;
                else
                    return Planes[_selectedPlaneIndex];
            }
        }

        private Button? _selectedButton;
        public int? SelectedButtonX
        {
            get
            {
                if (_selectedButton == null)
                    return null;
                else
                    return tableLayoutPanel.GetColumn(_selectedButton);
            }
        }
        public int? SelectedButtonY
        {
            get
            {
                if (_selectedButton == null)
                    return null;
                else
                    return tableLayoutPanel.GetRow(_selectedButton);
            }
        }
        public Point? SelectedButtonPoint
        {
            get
            {
                if (_selectedButton == null)
                    return null;
                else
                    return new Point((int)SelectedButtonX, (int)SelectedButtonY);
            }
        }

        public const int NumOfPlane = 3;
        public const int RowCount = 10;
        public const int ColumnCount = 10;

        private bool _planeVisible;
        public bool IsPlaneVisible
        {
            get
            {
                return _planeVisible;
            }
            set
            {
                if (Planes != null)
                    ClearPlanes();

                if (value)
                {
                    DrawPlanes();
                    foreach (Control control in this.tableLayoutPanel.Controls)
                    {
                        control.MouseClick += new MouseEventHandler(ButtonMouseClickForSelectPlane);
                        control.KeyPress += new KeyPressEventHandler(ButtonKeyPressForMove);
                    }
                }
                else
                {
                    foreach (Control control in this.tableLayoutPanel.Controls)
                    {
                        control.MouseClick -= new MouseEventHandler(ButtonMouseClickForSelectPlane);
                        control.KeyPress -= new KeyPressEventHandler(ButtonKeyPressForMove);
                    }
                }
                _planeVisible = value;
            }
        }

        public static Point ConvertStringToPoint(string str)
        {
            int Y = str[0] - 'A';
            int X = str[1] - '0';
            return new Point(X, Y);
        }

        public static string ConvertPointToString(int X, int Y)
        {
            return new string(new char[2] { (char)(Y + 'A'), (char)(X + '0') });
        }

        public static string ConvertPointToString(Point point)
        {
            return ConvertPointToString(point.Y, point.X);
        }

        public Control GetControlFromPosition(int X, int Y)
        {
            return tableLayoutPanel.GetControlFromPosition(X, Y);
        }

        public Control GetControlFromPosition(Point point)
        {
            return tableLayoutPanel.GetControlFromPosition(point.X, point.Y);
        }

        public Point GetControlPoint(Control control)
        {
            int X = tableLayoutPanel.GetColumn(control);
            int Y = tableLayoutPanel.GetRow(control);
            return new Point(X, Y);
        }

        public BombResult BombAndDraw(int X, int Y)
        {
            return BombAndDraw(new Point(X, Y));
        }

        public BombResult BombAndDraw(Point point)
        {
            Button selectedButton = (Button)GetControlFromPosition(point);
            if (selectedButton.BackColor != Color.Yellow)
            {
                selectedButton.BackColor = Color.Yellow;
                foreach (Plane plane in Planes)
                {
                    if (point.X == plane.HeadX && point.Y == plane.HeadY)
                    {
                        DrawBombResult(point, BombResult.head);
                        return BombResult.head;
                    }
                    else
                    {
                        foreach (Point planePoint in plane.Points)
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
            for (int i = 0; i < NumOfPlane; i++)
            {
                if (Planes[i].Points.Contains(point))
                {
                    _selectedPlaneIndex = i;
                    return;
                }
            }
            _selectedPlaneIndex = -1;
        }

        private void ButtonMouseClickForSelect(object sender, MouseEventArgs e)
        {
            _selectedButton = (Button)sender;
        }

        private void ButtonKeyPressForMove(object sender, KeyPressEventArgs e)
        {
            if (SelectedPlane != null)
            {
                switch (e.KeyChar)
                {
                    case 'w':
                        MovePlane(SelectedPlane, Direction.Up);
                        break;
                    case 's':
                        MovePlane(SelectedPlane, Direction.Down);
                        break;
                    case 'a':
                        MovePlane(SelectedPlane, Direction.Left);
                        break;
                    case 'd':
                        MovePlane(SelectedPlane, Direction.Right);
                        break;
                    case 'r':
                        RotatePlane(SelectedPlane);
                        break;
                }
            }
        }

        private bool MovePlane(Plane plane, Direction direction)
        {
            bool valid = true;
            Point origin = plane.HeadPoint;
            ClearPlanes();
            switch (direction)
            {
                case Direction.Up:
                    plane.HeadY -= 1;
                    break;
                case Direction.Down:
                    plane.HeadY += 1;
                    break;
                case Direction.Left:
                    plane.HeadX -= 1;
                    break;
                case Direction.Right:
                    plane.HeadX += 1;
                    break;
            }
            if (!CheckPlaneBounded(plane))
            {
                plane.HeadPoint = origin;
                valid = false;
            }
            DrawPlanes();
            return valid;
        }

        private bool RotatePlane(Plane plane)
        {
            bool valid = true;
            Direction origin = plane.direction;
            ClearPlanes();
            switch (plane.direction)
            {
                case Direction.Up:
                    plane.direction = Direction.Right;
                    break;
                case Direction.Down:
                    plane.direction = Direction.Left;
                    break;
                case Direction.Left:
                    plane.direction = Direction.Up;
                    break;
                case Direction.Right:
                    plane.direction = Direction.Down;
                    break;
            }
            if (!CheckPlaneBounded(plane))
            {
                plane.direction = origin;
                valid = false;
            }
            DrawPlanes();
            return valid;
        }

        public static bool CheckPlaneBounded(Plane plane)
        {
            foreach (var point in plane.Points)
                if (point.X < 0 || point.Y < 0 || point.X >= ColumnCount || point.Y >= RowCount)
                    return false;
            return true;
        }

        public static bool CheckPlaneNotConflicted(Plane plane, Plane[] planes)
        {
            foreach (Plane otherPlane in planes)
                if (!ReferenceEquals(otherPlane, plane) && plane.Points.Intersect(otherPlane.Points).Count() > 0)
                    return false;
            return true;
        }

        private bool CheckPlaneNotConflicted(Plane plane)
        {
            return CheckPlaneNotConflicted(plane, Planes);
        }

        public static bool CheckPlanesValid(Plane[] planes)
        {
            foreach (Plane plane in planes)
            {
                if (!CheckPlaneBounded(plane))
                    return false;
                if (!CheckPlaneNotConflicted(plane, planes))
                    return false;
            }
            return true;
        }

        public bool CheckPlanesValid()
        {
            return CheckPlanesValid(Planes);
        }

        public void ClearGrids()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                control.BackColor = SystemColors.ControlDark;
                control.ForeColor = SystemColors.ControlText;
                int X = tableLayoutPanel.GetColumn(control);
                int Y = tableLayoutPanel.GetRow(control);
                string column = X.ToString();
                string row = ((char)('A' + Y)).ToString();
                control.Text = row + column;
            }
            _selectedButton = null;
        }
        private void DrawPlanes()
        {
            foreach (Plane plane in Planes)
            {
                foreach (var point in plane.Points)
                {
                    Control control = GetControlFromPosition(point.X, point.Y);
                    if (control.BackColor != Color.Yellow)
                    {
                        if (control.BackColor == Color.LimeGreen)
                            control.BackColor = Color.DarkGreen;
                        else
                            control.BackColor = Color.LimeGreen;
                    }
                }
            }
            foreach (Plane plane in Planes)
            {
                Control control = GetControlFromPosition(plane.HeadPoint);
                if (control.BackColor != Color.Yellow)
                {
                    if (control.BackColor == Color.LimeGreen)
                        control.BackColor = Color.OrangeRed;
                    else
                        control.BackColor = Color.DarkRed;
                }
            }
        }

        public void ClearPlanes()
        {
            foreach (Plane plane in Planes)
            {
                foreach (var point in plane.Points)
                {
                    Control control = GetControlFromPosition(point.X, point.Y);
                    Color color = control.BackColor;
                    if (color == Color.LimeGreen || color == Color.DarkGreen || color == Color.OrangeRed)
                        control.BackColor = SystemColors.ControlDark;
                }
            }
        }


        public Point? GetBombPoint()
        {
            if (_selectedButton != null && _selectedButton.BackColor == Color.Yellow)
                throw new DuplicatedSelectionException("The button have been selected.");
            else
                return SelectedButtonPoint;
        }

        public List<(Point, BombResult)> GetCurrentGridStates()
        {
            List<(Point, BombResult)> results = new();
            foreach (Control control in tableLayoutPanel.Controls)
            {
                if (control.BackColor == Color.Yellow)
                {
                    if (control.Text == "身")
                    {
                        Point point = GetControlPoint(control);
                        results.Add((point, BombResult.body));

                    }
                    else if (control.Text == "头")
                    {
                        Point point = GetControlPoint(control);
                        results.Add((point, BombResult.head));
                    }
                }
            }
            return results;
        }

        public void InitializePlanes()
        {
            Random random = new();
            _planes = new Plane[NumOfPlane] {
                new Plane((Direction)random.Next(0, 3), random.Next(0, ColumnCount), random.Next(0, RowCount)),
                new Plane((Direction)random.Next(0, 3), random.Next(0, ColumnCount), random.Next(0, RowCount)),
                new Plane((Direction)random.Next(0, 3), random.Next(0, ColumnCount), random.Next(0, RowCount)),
            };
            int index = 0;
            while (!CheckPlanesValid(_planes))
                _planes[index++ % NumOfPlane] = new Plane((Direction)random.Next(0, 3), random.Next(0, ColumnCount), random.Next(0, RowCount));
            if (IsPlaneVisible)
                IsPlaneVisible = true;
        }
    }
    public class DuplicatedSelectionException : Exception
    {
        public DuplicatedSelectionException(string? message) : base(message) { }
    }
}