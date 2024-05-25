using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Loads.Enums;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Spans.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Factories
{
    internal static class LoadFactory
    {
        #region Public_Methods

        public static IContinousLoad CreateContinousLoad(
            ContinuousLoadType continousLoadType,
            ISpan span,
            double startPosition, double startValue,
            double endPosition, double endValue,
            double angle = 0)
        {
            switch (continousLoadType)
            {
                case ContinuousLoadType.AlongTemperatureDifferenceLoad:
                    return AlongTemperatureDifferenceLoad.Create(span, endValue - startValue);
                case ContinuousLoadType.ContinuousAngledLoad:
                    return ContinuousAngledLoad.Create(startPosition, startValue, endPosition, endValue, angle);
                case ContinuousLoadType.ContinuousBendingMomentLoad:
                    return ContinuousBendingMomentLoad.Create(span, endValue);
                case ContinuousLoadType.ContinuousNormalLoad:
                    return ContinuousNormalLoad.Create(startPosition, startValue, endPosition, endValue);
                case ContinuousLoadType.ContinuousShearLoad:
                    return ContinuousShearLoad.Create(startPosition, startValue, endPosition, endValue);
                case ContinuousLoadType.SpanExtendLoad:
                    return SpanExtendLoad.Create(span, endValue);
                case ContinuousLoadType.UpDownTemperatureDifferenceLoad:
                    return UpDownTemperatureDifferenceLoad.Create(span, endValue, startValue);
            }
            throw new NotSupportedException();
        }

        public static ISpanLoad CreatePointLoad(
            PointLoadType pointLoadType,
            double position, double value,
            double angle = 0)
        {
            switch (pointLoadType)
            {
                case PointLoadType.AngledLoad:
                    return new AngledLoad(value, position, angle);
                case PointLoadType.BendingMoment:
                    return new BendingMoment(value, position);
                case PointLoadType.HorizontalDisplacement:
                    return new HorizontalDisplacement(value);
                case PointLoadType.NormalLoad:
                    return new NormalLoad(value, position);
                case PointLoadType.RotationDisplacement:
                    return new RotationDisplacement(value);
                case PointLoadType.ShearLoad:
                    return new ShearLoad(value, position);
                case PointLoadType.VerticalDisplacement:
                    return new VerticalDisplacement(value);
            }
            throw new NotSupportedException();
        }

        #endregion // Public_Methods
    }
}
