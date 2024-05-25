using Build_IT_BeamStatica.Loads.ContinuousLoads.LoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads.ShearLoadResults
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
        {
            double distanceFromTheClosestLeftNode = distanceFromLeftSide - currentLength;

            if (distanceFromTheClosestLeftNode > ContinousLoad.EndPosition.Position)
                return CalculateRotationOutsideLoadLength(span, distanceFromLeftSide, currentLength);
            else if (distanceFromTheClosestLeftNode > ContinousLoad.StartPosition.Position)
                return CalculateRotationInsideLoadLength(span, distanceFromLeftSide, currentLength);
            return 0;
        }

        #endregion // Public_Methods

        #region Private_Methods

        private double CalculateRotationOutsideLoadLength(ISpan span,
         double distanceFromLeftSide, double currentLength)
        {
            double distanceFromTheClosestLeftNode = distanceFromLeftSide - currentLength;
            double forceAtX = GetForceAtTheCalculatedPoint(distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position);
            double rotation = 0;

            rotation += ContinousLoad.StartPosition.Value *
                (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 2 *
                (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 3 *
                (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) * 3 / 4
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            rotation += forceAtX *
                (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 2 *
                (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 3 *
                (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 4
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);

            rotation -= ContinousLoad.EndPosition.Value *
                (distanceFromTheClosestLeftNode - ContinousLoad.EndPosition.Position) / 2 *
                (distanceFromTheClosestLeftNode - ContinousLoad.EndPosition.Position) / 3 *
                (distanceFromTheClosestLeftNode - ContinousLoad.EndPosition.Position) * 3 / 4
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            rotation -= forceAtX *
                (distanceFromTheClosestLeftNode - ContinousLoad.EndPosition.Position) / 2 *
                (distanceFromTheClosestLeftNode - ContinousLoad.EndPosition.Position) / 3 *
                (distanceFromTheClosestLeftNode - ContinousLoad.EndPosition.Position) / 4
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);

            return rotation;
        }

        private double CalculateRotationInsideLoadLength(ISpan span,
            double distanceFromLeftSide, double currentLength)
        {
            double distanceFromTheClosestLeftNode = distanceFromLeftSide - currentLength;
            double forceAtX = GetForceAtTheCalculatedPoint(distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position);
            double rotation = 0;

            rotation += ContinousLoad.StartPosition.Value *
               (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 2 *
               (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 3 *
               (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) * 3 / 4
               / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            rotation += forceAtX *
                (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 2 *
                (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 3 *
                (distanceFromTheClosestLeftNode - ContinousLoad.StartPosition.Position) / 4
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);

            return rotation;
        }

        #endregion // Private_Methods
    }
}
