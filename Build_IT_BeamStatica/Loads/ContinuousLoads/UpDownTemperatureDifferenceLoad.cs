using Build_IT_BeamStatica.Loads.ContinuousLoads.UpDownTemperatureDifferenceResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads
{
    internal class UpDownTemperatureDifferenceLoad : ContinuousLoad
    {
        #region Properties

        private double _temperatureDifference => EndPosition.Value - StartPosition.Value;

        #endregion // Properties

        #region Factories

        public static IContinousLoad Create(ISpan span,
            double upperTemperature, double lowerTemperature)
        {
            return new UpDownTemperatureDifferenceLoad(
                           new LoadData(0, lowerTemperature),
                           new LoadData(span.Length, upperTemperature));
        }

        #endregion // Factories

        #region Constructors

        private UpDownTemperatureDifferenceLoad(ILoadWithPosition startPosition, ILoadWithPosition endPosition)
            : base(startPosition, endPosition)
        {
            RotationResult = new RotationResult(this);
            VerticalDeflectionResult = new VerticalDeflectionResult(this);
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateSpanLoadVectorBendingMomentMember(ISpan span, bool leftNode)
        {
            double sign = leftNode ? -1.0 : 1.0;
            return sign * span.Material.ThermalExpansionCoefficient * _temperatureDifference / span.Section.SolidHeight
                           * span.Section.MomentOfInteria * span.Material.YoungModulus * 10;
        }

        #endregion // Public_Methods
    }
}
