using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads.SpanExtendLoadResult;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads
{
    internal class SpanExtendLoad : ContinuousLoad
    {
        #region Factories

        public static IContinousLoad Create(ISpan span, double lengthIncrease)
        {
            return new SpanExtendLoad(
                           new LoadData(0, 0),
                           new LoadData(span.Length, lengthIncrease),
                           span.Material);
        }

        #endregion // Factories

        #region Constructors

        private SpanExtendLoad(
            ILoadWithPosition startPosition, ILoadWithPosition endPosition,
            MaterialData material)
            : base(startPosition, endPosition)
        {
            HorizontalDeflectionResult = new HorizontalDeflectionResult(this);
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateSpanLoadVectorNormalForceMember(ISpan span, bool leftNode)
        {
            double sign = leftNode ? 1.0 : -1.0;
            return sign * (this.EndPosition.Value - this.StartPosition.Value) / span.Length
               * span.Section.Area * span.Material.YoungModulus / 10;
        }

        #endregion // Public_Methods
    }
}
