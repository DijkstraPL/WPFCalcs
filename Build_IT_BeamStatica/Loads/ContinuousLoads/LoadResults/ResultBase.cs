using Build_IT_BeamStatica.Loads.Interfaces;
using System;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads.LoadResults
{
    internal abstract class ResultBase
    {
        #region Properties

        protected IContinousLoad ContinousLoad { get; }

        #endregion // Properties

        #region Constructors
        
        protected ResultBase(IContinousLoad continousLoad)
        {
            ContinousLoad = continousLoad ?? throw new ArgumentNullException(nameof(continousLoad));
        }

        #endregion // Constructors

        #region Protected_Methods
        
        protected double GetForceAtTheCalculatedPoint(double distanceFromLoadStartPosition)
            => (ContinousLoad.EndPosition.Value - ContinousLoad.StartPosition.Value) /
               ContinousLoad.Length *
               distanceFromLoadStartPosition +
               ContinousLoad.StartPosition.Value;

        #endregion // Protected_Methods
    }
}
