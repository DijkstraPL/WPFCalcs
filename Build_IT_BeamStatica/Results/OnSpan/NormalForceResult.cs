using Build_IT_BeamStatica.Beams.Interfaces;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Results.Reactions;
using Build_IT_BeamStatica.Spans.Interfaces;
using System.Linq;

namespace Build_IT_BeamStatica.Results.OnSpan
{
    internal class NormalForceResult : Result
    {
        #region Properties

        public IResultValue Result { get; private set; }

        #endregion // Properties

        #region Fields

        private double _currentLength;

        #endregion // Fields

        #region Constructors
        
        public NormalForceResult(IBeam beam) : base(beam)
        {
        }

        #endregion // Constructors

        #region Protected_Methods

        protected override IResultValue CalculateAtPosition(double distanceFromLeftSide)
        {
            Result = new NormalForce(distanceFromLeftSide) { Value = 0 };
            _currentLength = 0;

            CalculateNormalForce(distanceFromLeftSide);

            return Result;
        }

        #endregion // Protected_Methods

        #region Private_Methods

        private void CalculateNormalForce(double distanceFromLeftSide)
        {
            foreach (var span in Spans)
            {
                CalculateNormalForceFromNodeForces(span);
                CalculateNormalForceFromContinousLoads(distanceFromLeftSide, span);
                CalculateNormalForceFromPointLoads(distanceFromLeftSide, span);

                _currentLength += span.Length;
                if (distanceFromLeftSide <= _currentLength)
                    break;
            }
        }

        private void CalculateNormalForceFromNodeForces(ISpan span)
        {
            Result.Value += span.LeftNode.NormalForce?.Value ?? 0;
            Result.Value += span.LeftNode.ConcentratedForces.Sum(l => l.CalculateNormalForce());
        }

        private void CalculateNormalForceFromContinousLoads(double distanceFromLeftSide, ISpan span)
        {
            foreach (var load in span.ContinousLoads)
                if (distanceFromLeftSide - _currentLength > load.StartPosition.Position)
                    Result.Value += load.CalculateNormalForce(distanceFromLeftSide - load.StartPosition.Position - _currentLength);
        }

        private void CalculateNormalForceFromPointLoads(double distanceFromLeftSide, ISpan span)
        {
            foreach (var load in span.PointLoads)
                if (distanceFromLeftSide - _currentLength > load.Position)
                    Result.Value += load.CalculateNormalForce();
        }

        #endregion // Private_Methods
    }
}
