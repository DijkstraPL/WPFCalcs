using System;

namespace Build_IT_Data.Geometry
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo(Point point)
        {
            return Math.Sqrt(Math.Pow(this.X - point.X, 2) + Math.Pow(this.Y - point.Y, 2));
        }

        public Point GetPointAtDistance(Point directionPoint, double distance)
        {
            double distanceToDirectionPoint = DistanceTo(directionPoint);
            double x = X + distance / distanceToDirectionPoint * (directionPoint.X - X);
            double y = Y + distance / distanceToDirectionPoint * (directionPoint.Y - Y);

            return new Point(x, y);
        }
    }
}
