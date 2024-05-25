using Build_IT_BeamStatica.Loads.ContinuousLoads.LoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads.ShearLoadResults
{
    internal class BendingMomentResult : ForceResultBase
    {
        #region Constructors

        public BendingMomentResult(IContinousLoad continousLoad) : base(continousLoad)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public override double GetValue(double distanceFromLoadStartPosition)
        {
            if (distanceFromLoadStartPosition >= ContinousLoad.Length)
                return CalculateBendingMomentOutsideLoadLength(distanceFromLoadStartPosition);
            else
                return CalculateBendingMomentInsideLoadLength(distanceFromLoadStartPosition);
        }

        #endregion // Public_Methods

        #region Private_Methods

        private double CalculateBendingMomentOutsideLoadLength(double distanceFromLoadStartPosition)
        {
            double bendingMoment = 0;

            bendingMoment += ContinousLoad.StartPosition.Value *
                ContinousLoad.Length / 2 *
                (distanceFromLoadStartPosition - ContinousLoad.Length * 1 / 3);
            bendingMoment += ContinousLoad.EndPosition.Value *
                ContinousLoad.Length / 2 *
               (distanceFromLoadStartPosition - ContinousLoad.Length * 2 / 3);
            return bendingMoment;
        }

        private double CalculateBendingMomentInsideLoadLength(double distanceFromLoadStartPosition)
        {
            double forceAtX = GetForceAtTheCalculatedPoint(distanceFromLoadStartPosition);
            double bendingMoment = 0;

            bendingMoment += ContinousLoad.StartPosition.Value *
               distanceFromLoadStartPosition / 2 *
               distanceFromLoadStartPosition * 2 / 3;
            bendingMoment += forceAtX *
                distanceFromLoadStartPosition / 2 *
                distanceFromLoadStartPosition * 1 / 3;

            return bendingMoment;
        }

        #endregion // Private_Methods
    }
}
