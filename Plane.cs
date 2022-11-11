using System;
using System.Collections.Generic;
using System.Linq;
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

        public Direction direction;
        public Point HeadPoint;

        public int HeadX { get { return HeadPoint.X; } set { HeadPoint.X = value; } }
        public int HeadY { get { return HeadPoint.Y; } set { HeadPoint.Y = value; } }

        public HashSet<Point> Points
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
