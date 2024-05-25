using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Data
{
    public class Point 
    {
        #region Properties

        public double X { get; }
        public double Y { get; }

        #endregion // Properties

        #region Constructors

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion // Constructors

        #region Operators

        public static Point operator +(Point a, Point b)
            => new Point(a.X + b.X, a.Y + b.Y);

        public static Point operator -(Point a, Point b)
            => new Point(a.X - b.X, a.Y - b.Y);

        #endregion // Operators
    }
}
