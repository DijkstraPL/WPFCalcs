using System;

namespace Build_IT_BeamStaticaModule.Views.Preview.ContinuousLoads
{
        public class Arrow
        {
            public double Position { get; }
            public double Value { get; }

            public bool DrawArrow => _drawArrow?.Invoke(this) ?? false;
            public bool IsPositive => Value > 0;

            private readonly Predicate<Arrow> _drawArrow;

            public Arrow(double position, double value, Predicate<Arrow> drawArrow = null)
            {
                Position = position;
                Value = value;
                _drawArrow = drawArrow;
            }
        }
}
