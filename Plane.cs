using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BombPlane
{
    public class Plane
    {
        public Plane(Direction direction, int headX, int headY)
        {
            this.direction = direction;
            HeadPoint = new Point(headX, headY);
        }
        public Plane(Direction direction, Point headPoint)
        {
            this.direction = direction;
            HeadPoint = headPoint;
        }

        public bool Conflict(Plane plane) { return plane.Points.Overlaps(Points); }

        public Direction direction;
        public Point HeadPoint;

        public int HeadX { get { return HeadPoint.X; } set { HeadPoint.X = value; } }
        public int HeadY { get { return HeadPoint.Y; } set { HeadPoint.Y = value; } }


        private HashSet<Point> _points;
        public HashSet<Point> Points
        {
            get
            {
                if (_points == null)
                {
                    Point[] points;
                    switch (direction)
                    {
                        // 方向决定了头节点的朝向;旋转是绕着头节点的
                        case Direction.Up:
                            points = new Point[10] {
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
                            points = new Point[10] {
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
                            points = new Point[10] {
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
                            points = new Point[10] {
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
                            throw new Exception("Unexpected");
                    }
                    _points = new HashSet<Point>(points);
                }
                return _points;
            }
        }

        private HashSet<Point> _bodyPoints;
        public HashSet<Point> BodyPoints
        {
            get
            {
                if (_bodyPoints == null)
                {
                    _bodyPoints = new(Points);
                    _bodyPoints.Remove(HeadPoint);
                }
                return _bodyPoints;
            }
        }

        public const int WingLength = 2;
        public const int BodyLength = 4;
    }

    public enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }
}
