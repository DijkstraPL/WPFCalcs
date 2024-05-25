using Build_IT_BeamStatica.Beams.Interfaces;
using Build_IT_BeamStatica.Results.Displacements;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using System.Linq;

namespace Build_IT_BeamStatica.Results.OnSpan
{
    internal class RotationResult : Result
    {
        #region Properties

        public IResultValue Result { get; private set; }

        #endregion // Properties

        #region Fields

        private double _currentLength;
        private double _distanceFromLeftSide;

        private double _spanRotation;
        private readonly bool _adjustRotation;

        #endregion // Fields

        #region Constructors
        
        public RotationResult(IBeam beam) : base(beam)
        {
        }

        private RotationResult(IBeam beam, bool adjustRotation) : this(beam)
        {
            _adjustRotation = adjustRotation;
        }

        #endregion // Constructors

        #region Protected_Methods

        protected override IResultValue CalculateAtPosition(double distanceFromLeftSide)
        {
            _distanceFromLeftSide = distanceFromLeftSide;
            Result = new Rotation(_distanceFromLeftSide) { Value = 0 };

            _currentLength = 0;

            _spanRotation = 0;

            CalculateRotation();

            _spanRotation *= 100;

            Result.Value += _spanRotation;

            return Result;
        }

        #endregion // Protected_Methods

        #region Private_Methods
        
        private void CalculateRotation()
        {
            double calculatedLength = 0;
            foreach (var span in Beam.Spans)
            {
                calculatedLength += span.Length;
                if (calculatedLength <= _distanceFromLeftSide &&
                    !IsLastNode(span))
                {
                    _currentLength += span.Length;
                    continue;
                }

                if (_distanceFromLeftSide < _currentLength)
                {
                    _currentLength += span.Length;
                    continue;
                }

                CalculateRotationFromCalculatedForcesAndDisplacements(span);
                CalculateRotationFromNodeForces(span);
                CalculateRotationFromContinousLoads(span);
                CalculateRotationFromPointLoads(span);

                _currentLength += span.Length;
            }
        }

        private bool IsLastNode(ISpan span) =>
            span == Beam.Spans.Last() && _distanceFromLeftSide == Beam.Length;

        private void CalculateRotationFromCalculatedForcesAndDisplacements(ISpan span)
        {
            _spanRotation += span.LeftNode.RightRotation?.Value / 100 ?? 0;

            if (_currentLength != 0)
            {
                _spanRotation += Beam.Results.Shear.GetValue(_currentLength).Value
                * (_distanceFromLeftSide - _currentLength)
                * (_distanceFromLeftSide - _currentLength) / 2
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);

                _spanRotation += Beam.Results.BendingMoment.GetValue(_currentLength).Value
                    * (_distanceFromLeftSide - _currentLength)
                    / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            }
        }

        private void CalculateRotationFromNodeForces(ISpan span)
        {
            CalculateRotationFromMomentForces(span);
            CalculateRotationFromShearForces(span);
        }

        private void CalculateRotationFromContinousLoads(ISpan span)
        {
            _spanRotation += span.ContinousLoads.Sum(cl => 
            cl.CalculateRotation(span, _distanceFromLeftSide, _currentLength));
        }

        private void CalculateRotationFromPointLoads(ISpan span)
        {
            CalculateRotationFromRotationDisplacements(span);
            CalculateRotationFromShearForcesPointLoads(span);
            CalculateRotationFromBendingMomentPointLoads(span);
        }

        private void CalculateRotationFromRotationDisplacements(ISpan span)
        {
            _spanRotation += span.LeftNode.ConcentratedForces.Sum(cf
                => cf.CalculateRotationDisplacement()) / 100;
        }

        private void CalculateRotationFromShearForcesPointLoads(ISpan span)
        {
            foreach (var load in span.PointLoads)
            {
                if (_distanceFromLeftSide - _currentLength <= load.Position)
                    continue;

                _spanRotation += load.CalculateShear()
                    * (_distanceFromLeftSide - _currentLength - load.Position)
                    * (_distanceFromLeftSide - _currentLength - load.Position) / 2
                    / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            }
        }

        private void CalculateRotationFromBendingMomentPointLoads(ISpan span)
        {
            foreach (var load in span.PointLoads)
            {
                if (_distanceFromLeftSide - _currentLength <= load.Position)
                    continue;

                _spanRotation += load.CalculateBendingMoment(0)
                    * (_distanceFromLeftSide - _currentLength - load.Position)
                    / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            }
        }

        private void CalculateRotationFromMomentForces(ISpan span)
        {
            _spanRotation += (span.LeftNode.BendingMoment?.Value * (_distanceFromLeftSide - _currentLength) ?? 0)
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            _spanRotation += span.LeftNode.ConcentratedForces.Sum(cf => cf.CalculateBendingMoment(0)) *
                (_distanceFromLeftSide - _currentLength)
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
        }

        private void CalculateRotationFromShearForces(ISpan span)
        {
            _spanRotation += (span.LeftNode.ShearForce?.Value * (_distanceFromLeftSide - _currentLength) *
                (_distanceFromLeftSide - _currentLength) / 2 ?? 0)
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            _spanRotation += span.LeftNode.ConcentratedForces.Sum(cf => cf.CalculateShear()) *
                (_distanceFromLeftSide - _currentLength) * (_distanceFromLeftSide - _currentLength) / 2
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
        }

        #endregion // Private_Methods
    }
}
