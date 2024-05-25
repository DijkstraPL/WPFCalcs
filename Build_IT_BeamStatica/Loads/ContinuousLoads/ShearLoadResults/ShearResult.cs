using Build_IT_BeamStatica.Loads.ContinuousLoads.LoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads.ShearLoadResults
{
    internal class ShearResult : ForceResultBase
    {
        #region Constructors

        public ShearResult(IContinousLoad continousLoad) : base(continousLoad)
        {
        }

        #endregion // Constructors

        #region Public_Methods
        
        public override double GetValue(double distanceFromLoadStartPosition)
        {
            if (distanceFromLoadStartPosition >= ContinousLoad.Length)
                return CalculateShearForceOutsideLoadLength();
            else
                return CalculateShearForceInsideLoadLength(distanceFromLoadStartPosition);
        }

        #endregion // Public_Methods

        #region Private_Methods
        
        private double CalculateShearForceOutsideLoadLength()
            => ((ContinousLoad.StartPosition.Value + ContinousLoad.EndPosition.Value) * ContinousLoad.Length) / 2;

        private double CalculateShearForceInsideLoadLength(double distanceFromLoadStartPosition)
        {
            double lineAngle = (ContinousLoad.EndPosition.Value - ContinousLoad.StartPosition.Value) 
                / ContinousLoad.Length;

            return (ContinousLoad.StartPosition.Value + (lineAngle * distanceFromLoadStartPosition
                 + ContinousLoad.StartPosition.Value)) * distanceFromLoadStartPosition / 2;
        }

        #endregion // Private_Methods
    }
}
