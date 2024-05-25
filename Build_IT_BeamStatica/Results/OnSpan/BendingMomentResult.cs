using Build_IT_BeamStatica.Beams.Interfaces;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using System.Linq;

namespace Build_IT_BeamStatica.Results.OnSpan
{
    internal class BendingMomentResult : Result
    {
        #region Properties

        public IResultValue Result { get; private set; }

        #endregion // Properties

        #region Fields

        private double _currentLength;

        #endregion // Fields

        #region Constructors
        
        public BendingMomentResult(IBeam beam) : base(beam)
        {
        }

        #endregion //Constructors

        #region Protected_Methods
        
        protected override IResultValue CalculateAtPosition(double distanceFromLeftSide)
        {
            Result = new Reactions.BendingMoment(distanceFromLeftSide) { Value = 0 };
            _currentLength = 0;
            CalculateBendingMoment(distanceFromLeftSide);

            return Result;
        }

        #endregion // Protected_Methods

        #region Private_Methods

        private void CalculateBendingMoment(double distanceFromLeftSide)
        {
            foreach (var span in Spans)
            {
                CalculateBendingMomentFromNodeForces(distanceFromLeftSide, span);
                CalculateBendingMomentFromContinousLoads(distanceFromLeftSide, span);
                CalculateBendingMomentFromPointLoads(distanceFromLeftSide, span);

                _currentLength += span.Length;
                if (distanceFromLeftSide <= _currentLength)
                    break;
            }
        }

        private void CalculateBendingMomentFromNodeForces(double distanceFromLeftSide, ISpan span)
        {
            Result.Value += span.LeftNode.BendingMoment?.Value ?? 0;
            Result.Value += (span.LeftNode.ShearForce?.Value ?? 0) * (distanceFromLeftSide - _currentLength);
            Result.Value += span.LeftNode.ConcentratedForces.Sum(l => l.CalculateBendingMoment(distanceFromLeftSide - _currentLength));
        }

        private void CalculateBendingMomentFromContinousLoads(double distanceFromLeftSide, ISpan span)
        {
            foreach (var load in span.ContinousLoads)
                if (distanceFromLeftSide - _currentLength > load.StartPosition.Position)
                    Result.Value += load.CalculateBendingMoment(distanceFromLeftSide - load.StartPosition.Position - _currentLength);            
        }
        
        private void CalculateBendingMomentFromPointLoads(double distanceFromLeftSide, ISpan span)
        {
            foreach (var load in span.PointLoads)
                if (distanceFromLeftSide > load.Position + _currentLength)                    
                    Result.Value += load.CalculateBendingMoment(distanceFromLeftSide - load.Position - _currentLength);
        }

        #endregion // Private_Methods
    }
}
