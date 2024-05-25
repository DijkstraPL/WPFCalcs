using Build_IT_CommonTools.Extensions;
using Build_IT_Data.Geometry;
using Build_IT_Data.Geometry.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Build_IT_Data.Geometry
{
    public class Section
    {
        private readonly ObservableCollection<ContourPoint> _points = new();
        public IEnumerable<ContourPoint> Points => _points;

        private readonly List<Point> _calculationPoints = new();
        public IEnumerable<Point> CalculationPoints => _calculationPoints;

        public double SecondMomentOfInertiaX { get; private set; }
        public double SecondMomentOfInertiaY { get; private set; }
        public double ProductMomentOfInertiaXY { get; private set; }
        public double Circumference { get; private set; }
        public double Area { get; private set; }
        public Point CenterOfGravity { get; } = new Point(0, 0);
        public double RadiusOfGyrationX { get; private set; }
        public double RadiusOfGyrationY { get; private set; }
        public double PolarMomentOfInertia { get; private set; }
        public double PolarRadiusOfGyration { get; private set; }
        public double SectionModulusX { get; private set; }
        public double SectionModulusY { get; private set; }
        public double PrincipalAxisMomentOfInertiaX { get; private set; }
        public double PrincipalAxisMomentOfInertiaY { get; private set; }
        public double Angle { get; private set; }

        public Section()
        {
            _points.CollectionChanged += OnPointsChanged;
        }
        public void AddPoint(ContourPoint point)
        {
            _points.Add(point);
        }
        public void AddPoint(int index, ContourPoint point)
        {
            _points.Insert(index, point);
        }
        public void AddPoints(IEnumerable<ContourPoint> contourPoints)
        {
            _points.AddRange(contourPoints);
        }
        public void RemovePoint(ContourPoint point)
        {
            _points.Remove(point);
        }
        public void RemovePoint(int index)
        {
            _points.RemoveAt(index);
        }
        public void Clear()
        {
            _points.Clear();
        }

        private void OnPointsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Recalculate();
        }

        private void Recalculate()
        {
            var finalPoints = Discretize(Points);
            if (finalPoints is null || finalPoints.Count() <= 2)
            {
                SetToDefault();
                return;
            }

            _calculationPoints.Clear();
            _calculationPoints.AddRange(finalPoints);

            Area = GetArea(finalPoints);
            Circumference = GetCircumference(finalPoints);
            var centerOfGravity = GetCenterOfGravity(finalPoints);
            CenterOfGravity.X = centerOfGravity.X;
            CenterOfGravity.Y = centerOfGravity.Y;
            SecondMomentOfInertiaX = GetMomentOfInertiaX(finalPoints);
            SecondMomentOfInertiaY = GetMomentOfInertiaY(finalPoints);
            ProductMomentOfInertiaXY = GetProductMomentOfInertiaXY(finalPoints);
            PolarMomentOfInertia = GetPolarMomentOfInertia(finalPoints);
            RadiusOfGyrationX = GetRadiusOfGyrationX(finalPoints);
            RadiusOfGyrationY = GetRadiusOfGyrationY(finalPoints);
            PolarRadiusOfGyration = GetPolarRadiusOfGyration(finalPoints);
            SectionModulusX = GetSectionModulusX(finalPoints);
            SectionModulusY = GetSectionModulusY(finalPoints);
            PrincipalAxisMomentOfInertiaX = GetPrincipalAxisMomentOfInertiaX(finalPoints);
            PrincipalAxisMomentOfInertiaY = GetPrincipalAxisMomentOfInertiaY(finalPoints);
            Angle = GetAngle(finalPoints);
        }

        private void SetToDefault()
        {
            Area = double.NaN;
            Circumference = double.NaN;
            CenterOfGravity.X = double.NaN;
            CenterOfGravity.Y = double.NaN;
            SecondMomentOfInertiaX = double.NaN;
            SecondMomentOfInertiaY = double.NaN;
            ProductMomentOfInertiaXY = double.NaN;
            PolarMomentOfInertia = double.NaN;
            RadiusOfGyrationX = double.NaN;
            RadiusOfGyrationY = double.NaN;
            PolarRadiusOfGyration = double.NaN;
            SectionModulusX = double.NaN;
            SectionModulusY = double.NaN;
            PrincipalAxisMomentOfInertiaX = double.NaN;
            PrincipalAxisMomentOfInertiaY = double.NaN;
            Angle = double.NaN;
        }

        private List<Point> Discretize(IEnumerable<Point> points)
        {
            var pointList = points.ToList();
            var finalPoints = new List<Point>();

            for (int i = 0; i < pointList.Count; i++)
            {
                var point = pointList[i];
                if (point is ContourPoint contourPoint && contourPoint.PointType == PointType.Round)
                {
                    var previousPoint = i - 1 >= 0 ? pointList[i - 1] : pointList[^1];
                    var nextPoint = i + 1 < pointList.Count ? pointList[i + 1] : pointList[0];

                    var startingPoint = point.GetPointAtDistance(previousPoint, contourPoint.Value);
                    var endingPoint = point.GetPointAtDistance(nextPoint, contourPoint.Value);

                    var centerPoint1 = GetCenterPoint(startingPoint, endingPoint, contourPoint.Value, false);
                    var centerPoint2 = GetCenterPoint(startingPoint, endingPoint, contourPoint.Value, true);

                    Point centerPoint = point.DistanceTo(centerPoint1) <= contourPoint.Value ? centerPoint2 : centerPoint1;

                    double arcAngle = Math.Atan2(endingPoint.Y - centerPoint.Y, endingPoint.X - centerPoint.X)
                        - Math.Atan2(startingPoint.Y - centerPoint.Y, startingPoint.X - centerPoint.X);

                    if (arcAngle > Math.PI)
                        arcAngle = -(2 * Math.PI % arcAngle);
                    else if (arcAngle < -Math.PI)
                        arcAngle = 2 * Math.PI % arcAngle;
                    double spacingAngle = arcAngle / 10;
                    for (int j = 0; j <= 10; j++)
                    {
                        double newPointX = Math.Cos(spacingAngle * j) * (startingPoint.X - centerPoint.X) - Math.Sin(spacingAngle * j) * (startingPoint.Y - centerPoint.Y) + centerPoint.X;
                        double newPointY = Math.Sin(spacingAngle * j) * (startingPoint.X - centerPoint.X) + Math.Cos(spacingAngle * j) * (startingPoint.Y - centerPoint.Y) + centerPoint.Y;
                        finalPoints.Add(new Point(newPointX, newPointY));
                    }
                }
                else if (point is Point)
                    finalPoints.Add(point);
            }

            Point firstPoint = finalPoints.First();
            Point lastPoint = finalPoints.Last();
            if (firstPoint.X == lastPoint.X && firstPoint.Y == lastPoint.Y)
                finalPoints.RemoveAt(finalPoints.Count - 1);

            return finalPoints;
        }

        private Point GetCenterPoint(Point startPoint, Point endPoint, double radius, bool reverse)
        {
            double distance = startPoint.DistanceTo(endPoint);
            double baseX = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(distance / 2, 2)) * (startPoint.Y - endPoint.Y) / distance;
            double baseY = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(distance / 2, 2)) * (endPoint.X - startPoint.X) / distance;

            if (reverse)
            {
                baseX *= -1;
                baseY *= -1;
            }

            double xCenter = (startPoint.X + endPoint.X) / 2 + baseX;
            double yCenter = (startPoint.Y + endPoint.Y) / 2 + baseY;

            return new Point(xCenter, yCenter);
        }


        private double GetAngle(IEnumerable<Point> points)
        {
            return Math.Atan(-2 * GetProductMomentOfInertiaXY(points) / (GetMomentOfInertiaX(points) - GetMomentOfInertiaY(points))) * 180 / Math.PI / 2;
        }

        private double GetPrincipalAxisMomentOfInertiaY(IEnumerable<Point> points)
        {
            return (GetMomentOfInertiaX(points) + GetMomentOfInertiaY(points)) / 2
                 - Math.Sqrt(Math.Pow((GetMomentOfInertiaX(points) - GetMomentOfInertiaY(points)) / 2, 2) + Math.Pow(GetProductMomentOfInertiaXY(points), 2));
        }

        private double GetPrincipalAxisMomentOfInertiaX(IEnumerable<Point> points)
        {
            return (GetMomentOfInertiaX(points) + GetMomentOfInertiaY(points)) / 2
                 + Math.Sqrt(Math.Pow((GetMomentOfInertiaX(points) - GetMomentOfInertiaY(points)) / 2, 2) + Math.Pow(GetProductMomentOfInertiaXY(points), 2));
        }

        private double GetSectionModulusX(IEnumerable<Point> points)
        {
            var centerOfGravity = GetCenterOfGravity(points);
            var maxDistance = points.Max(p => Math.Abs(p.Y - centerOfGravity.Y));
            return GetMomentOfInertiaX(points) / maxDistance;
        }

        private double GetSectionModulusY(IEnumerable<Point> points)
        {
            var centerOfGravity = GetCenterOfGravity(points);
            var maxDistance = points.Max(p => Math.Abs(p.X - centerOfGravity.X));
            return GetMomentOfInertiaY(points) / maxDistance;
        }

        private double GetPolarMomentOfInertia(IEnumerable<Point> points)
        {
            return GetMomentOfInertiaX(points) + GetMomentOfInertiaY(points);
        }

        private double GetPolarRadiusOfGyration(IEnumerable<Point> points)
        {
            return Math.Sqrt(GetPolarMomentOfInertia(points) / GetArea(points));
        }

        private double GetProductMomentOfInertiaXY(IEnumerable<Point> points)
        {
            var pointList = points.ToList();
            Point centerOfGravity = GetCenterOfGravity(points);
            double sum = 0;
            for (int i = 0; i < pointList.Count; i++)
            {
                Point currentPoint = pointList[i];
                Point nextPoint = i + 1 < pointList.Count ? pointList[i + 1] : pointList[0];

                Point currentPointTransformed = new Point(currentPoint.X - centerOfGravity.X, currentPoint.Y - centerOfGravity.Y);
                Point nextPointTransformed = new Point(nextPoint.X - centerOfGravity.X, nextPoint.Y - centerOfGravity.Y);
                sum += (currentPointTransformed.X * nextPointTransformed.Y - currentPointTransformed.Y * nextPointTransformed.X)
                    * (currentPointTransformed.X * nextPointTransformed.Y
                        + 2 * currentPointTransformed.X * currentPointTransformed.Y
                        + 2 * nextPointTransformed.X * nextPointTransformed.Y
                        + currentPointTransformed.Y * nextPointTransformed.X);
            }
            return Math.Abs(sum / 24);
        }

        private double GetRadiusOfGyrationY(IEnumerable<Point> points)
        {
            return Math.Sqrt(GetMomentOfInertiaY(points) / GetArea(points));
        }

        private double GetRadiusOfGyrationX(IEnumerable<Point> points)
        {
            return Math.Sqrt(GetMomentOfInertiaX(points) / GetArea(points));
        }

        private Point GetCenterOfGravity(IEnumerable<Point> points)
        {
            var pointList = points.ToList();
            double sumX = 0, sumY = 0;
            for (int i = 0; i < pointList.Count; i++)
            {
                Point currentPoint = pointList[i];
                Point nextPoint = i + 1 < pointList.Count ? pointList[i + 1] : pointList[0];
                sumX += (currentPoint.X + nextPoint.X) *
                    (currentPoint.X * nextPoint.Y - currentPoint.Y * nextPoint.X);
                sumY += (currentPoint.Y + nextPoint.Y) *
                    (currentPoint.X * nextPoint.Y - currentPoint.Y * nextPoint.X);
            }
            return new Point(1.0 / (6 * GetArea(points)) * sumX, 1.0 / (6 * GetArea(points)) * sumY);
        }

        private double GetCircumference(IEnumerable<Point> points)
        {
            var pointList = points.ToList();
            double sum = 0;
            for (int i = 0; i < pointList.Count; i++)
            {
                Point currentPoint = pointList[i];
                Point nextPoint = i + 1 < pointList.Count ? pointList[i + 1] : pointList[0];
                sum += Math.Sqrt(Math.Pow(currentPoint.X - nextPoint.X, 2) + Math.Pow(currentPoint.Y - nextPoint.Y, 2));
            }
            return sum;
        }

        private double GetArea(IEnumerable<Point> points)
        {
            var pointList = points.ToList();
            double sum = 0;
            for (int i = 0; i < pointList.Count; i++)
            {
                Point currentPoint = pointList[i];
                Point nextPoint = i + 1 < pointList.Count ? pointList[i + 1] : pointList[0];
                sum += currentPoint.X * nextPoint.Y - currentPoint.Y * nextPoint.X;
            }
            return Math.Abs(sum / 2);
        }

        private double GetMomentOfInertiaX(IEnumerable<Point> points)
        {
            var pointList = points.ToList();
            Point centerOfGravity = GetCenterOfGravity(points);
            double sum = 0;
            for (int i = 0; i < pointList.Count; i++)
            {
                Point currentPoint = pointList[i];
                Point nextPoint = i + 1 < pointList.Count ? pointList[i + 1] : pointList[0];

                Point currentPointTransformed = new Point(currentPoint.X - centerOfGravity.X, currentPoint.Y - centerOfGravity.Y);
                Point nextPointTransformed = new Point(nextPoint.X - centerOfGravity.X, nextPoint.Y - centerOfGravity.Y);
                sum += (currentPointTransformed.X * nextPointTransformed.Y - currentPointTransformed.Y * nextPointTransformed.X)
                    * (Math.Pow(currentPointTransformed.Y, 2) + currentPointTransformed.Y * nextPointTransformed.Y + Math.Pow(nextPointTransformed.Y, 2));
            }
            return Math.Abs(sum / 12);
        }

        private double GetMomentOfInertiaY(IEnumerable<Point> points)
        {
            var pointList = points.ToList();
            Point centerOfGravity = GetCenterOfGravity(points);
            double sum = 0;
            for (int i = 0; i < pointList.Count; i++)
            {
                Point currentPoint = pointList[i];
                Point nextPoint = i + 1 < pointList.Count ? pointList[i + 1] : pointList[0];

                Point currentPointTransformed = new Point(currentPoint.X - centerOfGravity.X, currentPoint.Y - centerOfGravity.Y);
                Point nextPointTransformed = new Point(nextPoint.X - centerOfGravity.X, nextPoint.Y - centerOfGravity.Y);
                sum += (currentPointTransformed.X * nextPointTransformed.Y - currentPointTransformed.Y * nextPointTransformed.X)
                    * (Math.Pow(currentPointTransformed.X, 2) + currentPointTransformed.X * nextPointTransformed.X + Math.Pow(nextPointTransformed.X, 2));
            }
            return Math.Abs(sum / 12);
        }
    }
}
