using Build_IT_BeamStatica.Loads.ContinuousLoads.BendingMomentLoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads
{
    internal class ContinuousBendingMomentLoad : ContinuousLoad
    {
        #region Factories

        public static IContinousLoad Create(ISpan span, double value)
        {
            return new ContinuousBendingMomentLoad(
                new LoadData(0, value),
                new LoadData(span.Length, value));
        }

        #endregion // Factories

        #region Constructors

        private ContinuousBendingMomentLoad(ILoadWithPosition startPosition, ILoadWithPosition endPosition)
            : base(startPosition, endPosition)
        {
            BendingMomentResult = new BendingMomentResult(this);

            RotationResult = new RotationResult(this);
            VerticalDeflectionResult = new VerticalDeflectionResult(this);
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateSpanLoadVectorShearMember(ISpan span, bool leftNode)
        {
            double sign = leftNode ? -1.0 : 1.0;

            return sign * this.EndPosition.Value;
        }

        #endregion // Public_Methods
    }
}
