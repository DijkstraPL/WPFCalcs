using Build_IT_CommonTools.Attributes;
using Build_IT_Data.Sections.Additional;
using Build_IT_Data.Sections.Additional.Interfaces;
using Build_IT_Data.Sections.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_Data.Sections
{
    public class Section : ISection
    {
        [Abbreviation("I")]
        [Unit("cm4")]
        public double MomentOfInteria { get; protected set; }

        [Abbreviation("A")]
        [Unit("cm2")]
        public double Area { get; protected set; }

        [Abbreviation("C")]
        [Unit("cm")]
        public double Circumference { get; protected set; }

        public virtual double SolidHeight => Points.Max(p => p.Y) - Points.Min(p => p.Y);

        public IList<IPoint> Points { get; protected set; }
        public IList<IPoint> AdjustedPoints { get; protected set; }

        public IPoint Centroid { get; protected set; }

        protected Section()
        {
            Points = new List<IPoint>();
            AdjustedPoints = new List<IPoint>();
        }

        public Section(IList<IPoint> points)
        {
            if (points.Count < 3)
                throw new ArithmeticException("Less than 3 points");
            Points = points ?? throw new ArgumentNullException(nameof(points));
            AdjustedPoints = new List<IPoint>();

            SetSectionProperties();
        }

        public virtual void SetSectionProperties()
        {
            CalculateCimcuference();
            CalculateArea();
            CalculateCentroid();
            AdjustPoints();
            CalculateMomentOfInteria();
        }

        protected virtual void CalculateCimcuference()
        {
            double value = 0;
            for (int i = 0; i < Points.Count; i++)
            {
                int nextPointIndex = i + 1;
                if (nextPointIndex == Points.Count)
                    nextPointIndex = 0;

                value += Math.Sqrt(Math.Pow(Points[nextPointIndex].X - Points[i].X, 2)
                    + Math.Pow(Points[nextPointIndex].Y - Points[i].Y, 2));
            }
            Circumference = Math.Abs(value) / 10;
        }

        protected virtual void CalculateArea()
        {
            double value = 0;
            for (int i = 0; i < Points.Count; i++)
            {
                int nextPointIndex = i + 1;
                if (nextPointIndex == Points.Count)
                    nextPointIndex = 0;

                value += Points[i].X * Points[nextPointIndex].Y
                    - Points[nextPointIndex].X * Points[i].Y;
            }
            Area = 0.5 * Math.Abs(value) / 100;
        }

        protected virtual void CalculateCentroid()
        {
            double x = 0;
            double y = 0;
            for (int i = 0; i < Points.Count; i++)
            {
                int nextPointIndex = i + 1;
                if (nextPointIndex == Points.Count)
                    nextPointIndex = 0;

                x += (Points[i].X + Points[nextPointIndex].X)
                    * (Points[i].X * Points[nextPointIndex].Y
                    - Points[nextPointIndex].X * Points[i].Y);
                y += (Points[i].Y + Points[nextPointIndex].Y)
                    * (Points[i].X * Points[nextPointIndex].Y
                    - Points[nextPointIndex].X * Points[i].Y);
            }
            double value = 1.0 / (6 * Area * 100);

            Centroid = new Point(value * x, value * y);
        }

        protected virtual void AdjustPoints()
        {
            AdjustedPoints.Clear();
            foreach (var point in Points)
                AdjustedPoints.Add(SubstractPoints(point, Centroid));
        }

        protected virtual void CalculateMomentOfInteria()
        {
            double value = 0;
            for (int i = 0; i < AdjustedPoints.Count; i++)
            {
                int nextPointIndex = i + 1;
                if (nextPointIndex == AdjustedPoints.Count)
                    nextPointIndex = 0;

                value += (Math.Pow(AdjustedPoints[i].Y, 2)
                    + AdjustedPoints[i].Y * AdjustedPoints[nextPointIndex].Y
                    + Math.Pow(AdjustedPoints[nextPointIndex].Y, 2))
                    * (AdjustedPoints[i].X * AdjustedPoints[nextPointIndex].Y
                    - AdjustedPoints[nextPointIndex].X * AdjustedPoints[i].Y);
            }
            MomentOfInteria = 1.0 / 12 * Math.Abs(value) / 10000;
        }

        private IPoint SubstractPoints(IPoint a, IPoint b)
            => new Point(a.X - b.X, a.Y - b.Y);
    }
}
