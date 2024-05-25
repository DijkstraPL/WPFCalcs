using Build_IT_BeamStatica.Loads.ContinuousLoads.LoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads.SpanExtendLoadResult
{
    internal class HorizontalDeflectionResult : DisplacementResultBase
    {
        #region Constructors

        public HorizontalDeflectionResult(IContinousLoad continousLoad)
            : base(continousLoad)
        {
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override double GetValue(ISpan span, double distanceFromLeftSide, double currentLength)
            => -ContinousLoad.EndPosition.Value * (distanceFromLeftSide - currentLength) /100 ; // TODO: Check it!

        #endregion // Public_Methods
    }
}
