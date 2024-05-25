using Build_IT_BeamStatica.Loads.ContinuousLoads.LoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads.NormalLoadResults
{
    internal class HorizontalDeflectionResult : DisplacementResultBase
    {
        #region Constructors

        public HorizontalDeflectionResult(IContinousLoad continousLoad) : base(continousLoad)
        {
        }

        #endregion //Constructors

        #region Public_Methods
        
        public override double GetValue(ISpan span, double distanceFromLeftSide, double currentLength)
        {
            if (distanceFromLeftSide > ContinousLoad.EndPosition.Position + currentLength)
                return CalculateDeflectionOutsideLoadLength(span, distanceFromLeftSide, currentLength);
            else if (distanceFromLeftSide > ContinousLoad.StartPosition.Position + currentLength)
                return CalculateDeflectionInsideLoadLength(span, distanceFromLeftSide, currentLength);
            return 0;
        }

        #endregion // Public_Methods

        #region Private_Methods

        private double CalculateDeflectionOutsideLoadLength(ISpan span, double distanceFromLeftSide, double currentLength)
        {
            double distanceFromTheClosestLeftNode = distanceFromLeftSide - currentLength;
            double forceAtX = GetForceAtTheCalculatedPoint(distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position);
            double deflection = 0;

            deflection += ContinousLoad.StartPosition.Value
                * (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 2
                * (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) * 2 / 3
                / (span.Material.YoungModulus * span.Section.Area);
            deflection += forceAtX
                * (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 2
                * (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 3
                / (span.Material.YoungModulus * span.Section.Area);

            deflection -= ContinousLoad.EndPosition.Value
                * (distanceFromTheClosestLeftNode - ContinousLoad.EndPosition.Position) / 2
                * (distanceFromTheClosestLeftNode - ContinousLoad.EndPosition.Position) * 2 / 3
                / (span.Material.YoungModulus * span.Section.Area);
            deflection -= forceAtX
                * (distanceFromTheClosestLeftNode - ContinousLoad.EndPosition.Position) / 2
                * (distanceFromTheClosestLeftNode - ContinousLoad.EndPosition.Position) / 3
                / (span.Material.YoungModulus * span.Section.Area);

            return deflection;
        }

        private double CalculateDeflectionInsideLoadLength(ISpan span, double distanceFromLeftSide, double currentLength)
        {
            double distanceFromTheClosestLeftNode = distanceFromLeftSide - currentLength;
            double forceAtX = GetForceAtTheCalculatedPoint(distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position);
            double deflection = 0;

            deflection += ContinousLoad.StartPosition.Value
                * (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 2
                * (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) * 2 / 3
                / (span.Material.YoungModulus * span.Section.Area);
            deflection += forceAtX
                * (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 2
                * (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 3
                / (span.Material.YoungModulus * span.Section.Area);

            return deflection;
        }

        #endregion // Private_Methods
    }
}
