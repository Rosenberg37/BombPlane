﻿using System;
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
    public partial class GridNetwork : UserControl
    {
        public GridNetwork()
        {
            InitializeComponent();
            _planes = new Plane[NumOfPlane] {
                new Plane(Direction.Up, 2, 0),
                new Plane(Direction.Up, 2, 6),
                new Plane(Direction.Up, 7, 2)
            };
            foreach (Control control in tableLayoutPanel.Controls)
                control.MouseClick += new MouseEventHandler(ButtonMouseClickForSelect);
        }

        public Control GetControlFromPosition(int X, int Y)
        {
            return tableLayoutPanel.GetControlFromPosition(X, Y);
        }

        public Control GetControlFromPosition(Point point)
        {
            return tableLayoutPanel.GetControlFromPosition(point.X, point.Y);
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

        private void ButtonMouseClickForSelect(object sender, MouseEventArgs e)
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
                        MovePlane(_selectedPlane, Direction.Up);
                        break;
                    case 's':
                        MovePlane(_selectedPlane, Direction.Down);
                        break;
                    case 'a':
                        MovePlane(_selectedPlane, Direction.Left);
                        break;
                    case 'd':
                        MovePlane(_selectedPlane, Direction.Right);
                        break;
                    case 'r':
                        RotatePlane(_selectedPlane);
                        break;
                }
            }
        }

        private bool MovePlane(Plane plane, Direction direction)
        {
            bool valid = true;
            Point origin = plane.HeadPoint;
            ClearGrids();
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
            ClearGrids();
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
            foreach (var point in plane.BodyPoints)
                if (point.X < 0 || point.Y < 0 || point.X >= ColumnCount || point.Y >= RowCount)
                    return false;
            return true;
        }

        public static bool CheckPlaneNotConflicted(Plane plane, Plane[] planes)
        {
            foreach (Plane otherPlane in planes)
                if (!ReferenceEquals(otherPlane, plane) && plane.BodyPoints.Intersect(otherPlane.BodyPoints).Count() > 0)
                    return false;
            return true;
        }

        private bool CheckPlaneNotConflicted(Plane plane)
        {
            return CheckPlaneNotConflicted(plane, _planes);
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
            return CheckPlanesValid(_planes);
        }

        private void DrawPlanes()
        {
            foreach (Plane plane in _planes)
            {
                foreach (var point in plane.BodyPoints)
                {
                    Control control = GetControlFromPosition(point.X, point.Y);
                    if (control.BackColor != Color.OrangeRed)
                    {
                        if (control.BackColor == Color.LimeGreen)
                            control.BackColor = Color.DarkGreen;
                        else
                            control.BackColor = Color.LimeGreen;
                    }
                }
            }
            foreach (Plane plane in _planes)
            {
                Control headControl = GetControlFromPosition(plane.HeadPoint);
                if (headControl.BackColor == Color.LimeGreen)
                    headControl.BackColor = Color.OrangeRed;
                else
                    headControl.BackColor = Color.DarkRed;
            }
        }

        public void ClearGrids()
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

        public Point GetSelectBombPoint()
        {
            if (_selectedButton.BackColor != SystemColors.ControlDark)
                throw new DuplicatedSelectionException("The button have been selected.");
            else
                return SelectedButtonPoint;
        }



        internal Plane[] _planes;
        internal Plane? _selectedPlane;
        private Button _selectedButton;
        public int SelectedButtonX { get { return tableLayoutPanel.GetColumn(_selectedButton); } }
        public int SelectedButtonY { get { return tableLayoutPanel.GetRow(_selectedButton); } }
        public Point SelectedButtonPoint { get { return new Point(SelectedButtonX, SelectedButtonY); } }

        public const int NumOfPlane = 3;
        public const int RowCount = 10;
        public const int ColumnCount = 10;

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
                    DrawPlanes();
                    foreach (Control control in this.tableLayoutPanel.Controls)
                    {
                        control.MouseClick += new MouseEventHandler(ButtonMouseClickForSelectPlane);
                        control.KeyPress += new KeyPressEventHandler(ButtonKeyPressForMove);
                    }
                }
                else
                {
                    ClearGrids();
                    foreach (Control control in this.tableLayoutPanel.Controls)
                    {
                        control.MouseClick -= new MouseEventHandler(ButtonMouseClickForSelectPlane);
                        control.KeyPress -= new KeyPressEventHandler(ButtonKeyPressForMove);
                    }
                }
            }
        }

    }
    public class DuplicatedSelectionException : Exception
    {
        public DuplicatedSelectionException(string? message) : base(message) { }
    }

    public class Plane
    {
        public Plane(Direction direction, int headX, int headY)
        {
            this.direction = direction;
            HeadPoint = new Point(headX, headY);
        }

        public Direction direction;
        public Point HeadPoint;

        public int HeadX { get { return HeadPoint.X; } set { HeadPoint.X = value; } }
        public int HeadY { get { return HeadPoint.Y; } set { HeadPoint.Y = value; } }

        public HashSet<Point> BodyPoints
        {
            get
            {
                Point[] points;
                switch (direction)
                {
                    // direction decide the head of plane fact to;
                    // rotation is centered on head.
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

    public enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }
}