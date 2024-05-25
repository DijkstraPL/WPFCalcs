using Build_IT_BeamStatica.Loads.ContinuousLoads.Interfaces;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads.LoadResults
{
    internal abstract class DisplacementResultBase : ResultBase, IDisplacementResult
    {
        #region Constructors

        protected DisplacementResultBase(IContinousLoad continousLoad) : base(continousLoad)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public abstract double GetValue(ISpan span, double distanceFromLeftSide, double currentLength);

        #endregion // Public_Methods
    }
}
