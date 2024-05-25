using Build_IT_BeamStatica.Beams.Interfaces;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Results.Reactions;
using Build_IT_BeamStatica.Spans.Interfaces;
using System.Linq;

namespace Build_IT_BeamStatica.Results.OnSpan
{
    internal class ShearResult : Result
    {
        #region Properties

        public IResultValue Result { get; private set; }

        #endregion // Properties

        #region Fields

        private double _currentLength;

        #endregion // Fields

        #region Constructors
        
        public ShearResult(IBeam beam) : base(beam)
        {
        }

        #endregion // Constructors

        #region Protected_Methods
        
        protected override IResultValue CalculateAtPosition(double distanceFromLeftSide)
        {
            Result = new ShearForce(distanceFromLeftSide) { Value = 0 };
            _currentLength = 0;

            CalculateShear(distanceFromLeftSide);

            return Result;
        }

        #endregion // Protected_Methods

        #region Private_Methods
        
        private void CalculateShear(double distanceFromLeftSide)
        {
            foreach (var span in Spans)
            {
                CalculateShearForceFromNodeForces(span);
                CalculateShearFromContinousLoads(distanceFromLeftSide, span);
                CalculateShearFromPointLoads(distanceFromLeftSide, span);

                _currentLength += span.Length;
                if (distanceFromLeftSide <= _currentLength)
                    break;
            }
        }

        private void CalculateShearForceFromNodeForces(ISpan span)
        {
            Result.Value += span.LeftNode.ShearForce?.Value ?? 0;
            Result.Value += span.LeftNode.ConcentratedForces.Sum(l => l.CalculateShear());
        }

        private void CalculateShearFromContinousLoads(double distanceFromLeftSide, ISpan span)
        {
            foreach (var load in span.ContinousLoads)
                if (distanceFromLeftSide - _currentLength > load.StartPosition.Position)
                    Result.Value += load.CalculateShear(distanceFromLeftSide - load.StartPosition.Position - _currentLength);
        }

        private void CalculateShearFromPointLoads(double distanceFromLeftSide, ISpan span)
        {
            foreach (var load in span.PointLoads)
                if (distanceFromLeftSide - _currentLength > load.Position)
                    Result.Value += load.CalculateShear();
        }

        #endregion // Private_Methods
    }
}
