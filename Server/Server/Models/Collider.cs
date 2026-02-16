using Microsoft.AspNetCore.Connections;
using System.Numerics;

namespace Server.Models
{
    public interface ICollider
    {
        public static double DistancePointToLine(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
        {
            double x0 = point.X, y0 = point.Y;
            double x1 = lineStart.X, y1 = lineStart.Y;
            double x2 = lineEnd.X, y2 = lineEnd.Y;

            if (x1 == x2 && y1 == y2)
            {
                double dx = x0 - x1;
                double dy = y0 - y1;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            double crossProduct = Math.Abs(
                (x2 - x1) * (y1 - y0) - (x1 - x0) * (y2 - y1)
            );

            double lineLength = Math.Sqrt(
                (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)
            );

            return crossProduct / lineLength;
        }
        public bool CheckCollision(ICollider collider);
        public void Move(Vector2 Move);
        
        public static bool SegmentsIntersect(LineCollider Line1, LineCollider Line2)
        {
            float o1 = Orientation(Line1.Point1, Line1.Point2, Line2.Point1);
            float o2 = Orientation(Line1.Point1, Line1.Point2, Line2.Point2);
            float o3 = Orientation(Line2.Point1, Line2.Point2, Line1.Point1);
            float o4 = Orientation(Line2.Point1, Line2.Point2, Line1.Point2);

            if (o1 * o2 < 0 && o3 * o4 < 0)
            {
                return true;
            }

            return false;
        }
        private static float Orientation(Vector2 p, Vector2 q, Vector2 r)
        {
            return (q.X - p.X) * (r.Y - p.Y) - (q.Y - p.Y) * (r.X - p.X);
        }
    }

    public class BoxCollider : ICollider
    {
        private LineCollider[] lineColliders = new LineCollider[4];

        public BoxCollider(Vector2 Left_up, Vector2 Right_up, Vector2 Left_down, Vector2 Right_down)
        {
            lineColliders[0] = new LineCollider(Left_up, Right_up);
            lineColliders[1] = new LineCollider(Right_up, Right_down);
            lineColliders[2] = new LineCollider(Right_down, Left_down);
            lineColliders[3] = new LineCollider(Left_down, Left_up);
        }
        public LineCollider[] GetLineColliders()
        {
            return lineColliders;
        }
        public void Move(Vector2 Move)
        {
            for (int i = 0; i < lineColliders.Length; i++)
            {
                lineColliders[i].Move(Move);
            }         
        }

        public bool CheckCollision(ICollider collider)
        {
            for (int i = 0; i < lineColliders.Length; i++)
            {
                if (lineColliders[i].CheckCollision(collider))
                    return true;
            }
            return false;
        }
    }

    public class CircleCollider : ICollider
    {
        public Vector2 Center { get; private set; }
        public double Radius { get; private set; }

        public CircleCollider(Vector2 Center, double Radius)
        {
            this.Center = Center;
            this.Radius = Radius;
        }

        public void Move(Vector2 Move)
        {
            Center += Move;
        }

        public bool CheckCollision(ICollider collider)
        {
            if (collider is CircleCollider && collider != null)
            {
                CircleCollider circle_collider = collider as CircleCollider ?? throw new Exception("Circle collider is null");
                return Math.Pow((Center.X - circle_collider.Center.X), 2) + Math.Pow((Center.Y - circle_collider.Center.Y), 2) <=  Math.Pow(Math.Max(Radius, circle_collider.Radius), 2);
            }
            else if (collider is LineCollider && collider != null)
            {
                LineCollider line_collider = collider as LineCollider ?? throw new Exception("Line collider is null");
                return line_collider.CheckCollision(this);
            }
            else if (collider is BoxCollider && collider != null)
            {
                BoxCollider box_collider = collider as BoxCollider ?? throw new Exception("Box collider is null");
                return box_collider.CheckCollision(this);
            }
            else if (collider == null)
                throw new Exception("Collider is null");
            else
                throw new Exception("Unknown collider");
        }
    }

    public class LineCollider : ICollider
    {
        public Vector2 Point1 { get; set; }
        public Vector2 Point2 { get; set; }

        public double Angle;

        public LineCollider(Vector2 Point1, Vector2 Point2)
        {
            this.Point1 = Point1;
            this.Point2 = Point2;
            Angle = Math.Atan((Point2.Y - Point1.Y) / (Point2.X - Point1.X));
            Ball.LineColliders.Add(this);
        }

        public bool CheckCollision(ICollider collider)
        {
            if (collider is CircleCollider && collider != null)
            {
                CircleCollider circle_collider = collider as CircleCollider ?? throw new Exception("Circle collider is null");
                return ICollider.DistancePointToLine(circle_collider.Center, Point1, Point2) <= circle_collider.Radius;
            }
            else if (collider is LineCollider && collider != null)
            {
                LineCollider line_collider = collider as LineCollider ?? throw new Exception("Line collider is null");
                return ICollider.SegmentsIntersect(this, line_collider);
            }
            else if (collider is BoxCollider && collider != null)
            {
                BoxCollider box_collider = collider as BoxCollider ?? throw new Exception("Box collider is null");
                LineCollider[] line_colliders = box_collider.GetLineColliders();
                for (int i = 0; i < line_colliders.Length; i++)
                {
                    if (CheckCollision(line_colliders[i]))
                        return true;
                }
                return false;
            }
            else if (collider == null)
                throw new Exception("Collider is null");
            else
                throw new Exception("Unknown collider");
        }

        public void Move(Vector2 Move)
        {
            Point1 += Move;
            Point2 += Move;
        }

    }
}
