using Build_IT_BeamStatica.Builders.Interfaces;
using Build_IT_BeamStatica.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_BeamStatica.Builders
{
    public class SectionPropertiesBuilder : ISectionPropertiesBuilder
    {
        #region Fields

        private  SectionData _sectionPropertiesData;
        private List<Point> _points = new List<Point>();
        private List<Point> _adjustedPoints = new List<Point>();

        private double _circumference;
        private double _area;
        private double _momentOfInteria;
        private Point _centroid;

        #endregion // Fields

        #region Constructors

        internal SectionPropertiesBuilder()
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public SectionPropertiesBuilder WithPoint(double x, double y)
        {
            _points.Add(new Point(x, y));
            return this;
        }

        public SectionPropertiesBuilder ClearPoints()
        {
            _points.Clear();
            return this;
        }

        public SectionData Build()
        {
            if (!Validate())
                throw new InvalidOperationException();
            
            SetSectionProperties();
            _sectionPropertiesData = new SectionData
            {
                Area = _area,
                Circumference = _circumference,
                SolidHeight = _points.Max(p=>p.Y) - _points.Min(p=> p.Y) ,
                MomentOfInteria = _momentOfInteria
            };
            return _sectionPropertiesData;
        }

        public bool Validate()
        {
            return _points.Count > 2;
        }

        #endregion // Public_Methods

        #region Private_Methods
        private void SetSectionProperties()
        {
            CalculateCimcuference();
            CalculateArea();
            CalculateCentroid();
            AdjustPoints();
            CalculateMomentOfInteria();
        }

        private void CalculateCimcuference()
        {
            double value = 0;
            for (int i = 0; i < _points.Count; i++)
            {
                int nextPointIndex = i + 1;
                if (nextPointIndex == _points.Count)
                    nextPointIndex = 0;

                value += Math.Sqrt(Math.Pow(_points[nextPointIndex].X - _points[i].X, 2)
                    + Math.Pow(_points[nextPointIndex].Y - _points[i].Y, 2));
            }
            _circumference = Math.Abs(value) / 10;
        }

        private void CalculateArea()
        {
            double value = 0;
            for (int i = 0; i < _points.Count; i++)
            {
                int nextPointIndex = i + 1;
                if (nextPointIndex == _points.Count)
                    nextPointIndex = 0;

                value += _points[i].X * _points[nextPointIndex].Y
                    - _points[nextPointIndex].X * _points[i].Y;
            }
            _area = 0.5 * Math.Abs(value) / 100;
        }

        private void CalculateCentroid()
        {
            double x = 0;
            double y = 0;
            for (int i = 0; i < _points.Count; i++)
            {
                int nextPointIndex = i + 1;
                if (nextPointIndex == _points.Count)
                    nextPointIndex = 0;

                x += (_points[i].X + _points[nextPointIndex].X)
                    * (_points[i].X * _points[nextPointIndex].Y
                    - _points[nextPointIndex].X * _points[i].Y);
                y += (_points[i].Y + _points[nextPointIndex].Y)
                    * (_points[i].X * _points[nextPointIndex].Y
                    - _points[nextPointIndex].X * _points[i].Y);
            }
            double value = 1.0 / (6 * _area * 100);

            _centroid = new Point(value * x, value * y);
        }

        private void AdjustPoints()
        {
            _adjustedPoints.Clear();
            foreach (var point in _points)
                _adjustedPoints.Add(SubstractPoints(point, _centroid));
        }

        private  void CalculateMomentOfInteria()
        {
            double value = 0;
            for (int i = 0; i < _adjustedPoints.Count; i++)
            {
                int nextPointIndex = i + 1;
                if (nextPointIndex == _adjustedPoints.Count)
                    nextPointIndex = 0;

                value += (Math.Pow(_adjustedPoints[i].Y, 2)
                    + _adjustedPoints[i].Y * _adjustedPoints[nextPointIndex].Y
                    + Math.Pow(_adjustedPoints[nextPointIndex].Y, 2))
                    * (_adjustedPoints[i].X * _adjustedPoints[nextPointIndex].Y
                    - _adjustedPoints[nextPointIndex].X * _adjustedPoints[i].Y);
            }
            _momentOfInteria = 1.0 / 12 * Math.Abs(value) / 10000;
        }

        private Point SubstractPoints(Point a, Point b)
            => new Point(a.X - b.X, a.Y - b.Y);

        #endregion // Private_Methods
    }
}
