using Build_IT_BeamStatica.Loads.ContinuousLoads.LoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads.UpDownTemperatureDifferenceResults
{
    internal class RotationResult : DisplacementResultBase
    {
        #region Constructors

        public RotationResult(IContinousLoad continousLoad) : base(continousLoad)
        {
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override double GetValue(ISpan span, double distanceFromLeftSide, double currentLength)
         => (ContinousLoad.StartPosition.Value - ContinousLoad.EndPosition.Value)
                / span.Section.SolidHeight
                * (distanceFromLeftSide - currentLength) / 10000;

        #endregion // Public_Methods
    }
}
