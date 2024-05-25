using Build_IT_BeamStatica.Beams.Interfaces;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Results.Displacements;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using System.Linq;

namespace Build_IT_BeamStatica.Results.OnSpan
{
    internal class VerticalDeflectionResult : Result
    {
        #region Properties

        public IResultValue Result { get; private set; }

        #endregion // Properties

        #region Fields
        
        private const double _nextToNodePosition = 0.00000001;

        private double _currentLength;
        private double _distanceFromLeftSide;

        private double _spanDeflection;

        #endregion // Fields

        #region Constructors
        
        public VerticalDeflectionResult(IBeam beam) : base(beam)
        {
        }

        #endregion // Constructors

        #region Protected_Methods
        
        protected override IResultValue CalculateAtPosition(double distanceFromLeftSide)
        {
            _distanceFromLeftSide = distanceFromLeftSide;
            Result = new Rotation(distanceFromLeftSide) { Value = 0 };

            _currentLength = 0;

            _spanDeflection = 0;

            CalculateDeflection();

            _spanDeflection *= 100000;
            Result.Value += _spanDeflection;

            return Result;
        }

        #endregion // Protected_Methods

        #region Private_Methods

        private void CalculateDeflection()
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

                if (_distanceFromLeftSide >= _currentLength)
                {
                    CalculateDeflectionFromCalculatedForcesAndDisplacements(span);
                    CalculateDeflectionFromNodeForces(span);
                    CalculateDeflectionFromContinousLoads(span);
                    CalculateDeflectionFromPointLoads(span);
                }
                _currentLength += span.Length;
            }
        }
        
        private bool IsLastNode(ISpan span) =>
            span == Beam.Spans.Last() && _distanceFromLeftSide == Beam.Length;

        private void CalculateDeflectionFromCalculatedForcesAndDisplacements(ISpan span)
        {
            _spanDeflection += span.LeftNode.VerticalDeflection?.Value / 100000 ?? 0;
            _spanDeflection += span.LeftNode.RightRotation?.Value * (_distanceFromLeftSide - _currentLength) / 100 ?? 0;

            if (_currentLength != 0)
            {
                _spanDeflection += Beam.Results.Shear.GetValue(_currentLength).Value
                    * (_distanceFromLeftSide - _currentLength)
                    * (_distanceFromLeftSide - _currentLength) / 2
                    * (_distanceFromLeftSide - _currentLength) / 3
                    / (span.Material.YoungModulus * span.Section.MomentOfInteria);

                _spanDeflection += Beam.Results.BendingMoment.GetValue(_currentLength).Value
                    * (_distanceFromLeftSide - _currentLength)
                    * (_distanceFromLeftSide - _currentLength) / 2
                    / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            }
        }

        private void CalculateDeflectionFromNodeForces(ISpan span)
        {
            CalculateDeflectionFromMomentForces(span);
            CalculateDeflectionFromShearForces(span);
        }

        private void CalculateDeflectionFromContinousLoads(ISpan span)
        {
            _spanDeflection += span.ContinousLoads.Sum(cl =>
            cl.CalculateVerticalDeflection(span, _distanceFromLeftSide, _currentLength));
        }

        private void CalculateDeflectionFromPointLoads(ISpan span)
        {
            CalculateRotationFromRotationDisplacements(span);
            CalculateDeflectionFromVerticalDisplacements(span);
            CalculateDeflectionFromShearForcesPointLoads(span);
            CalculateDeflectionFromBendingMomentPointLoads(span);
        }

        private void CalculateDeflectionFromShearForcesPointLoads(ISpan span)
        {
            foreach (var load in span.PointLoads)
            {
                if (_distanceFromLeftSide - _currentLength <= load.Position)
                    continue;

                _spanDeflection += load.CalculateShear()
                    * (_distanceFromLeftSide - _currentLength - load.Position)
                    * (_distanceFromLeftSide - _currentLength - load.Position) / 2
                    * (_distanceFromLeftSide - _currentLength - load.Position) / 3
                    / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            }
        }

        private void CalculateDeflectionFromBendingMomentPointLoads(ISpan span)
        {
            foreach (var load in span.PointLoads)
            {
                if (_distanceFromLeftSide - _currentLength <= load.Position)
                    continue;

                _spanDeflection += load.CalculateBendingMoment(0)
                    * (_distanceFromLeftSide - _currentLength - load.Position)
                    * (_distanceFromLeftSide - _currentLength - load.Position) / 2
                    / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            }
        }

        private void CalculateDeflectionFromShearForces(ISpan span)
        {
            _spanDeflection += (span.LeftNode.ShearForce?.Value
                * (_distanceFromLeftSide - _currentLength)
                * (_distanceFromLeftSide - _currentLength) / 2
                * (_distanceFromLeftSide - _currentLength) / 3
                ?? 0)
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            _spanDeflection += span.LeftNode.ConcentratedForces.Sum(cf => cf.CalculateShear())
                * (_distanceFromLeftSide - _currentLength)
                * (_distanceFromLeftSide - _currentLength) / 2
                * (_distanceFromLeftSide - _currentLength) / 3
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
        }

        private void CalculateRotationFromRotationDisplacements(ISpan span)
        {
            _spanDeflection += span.LeftNode.ConcentratedForces.Sum(cf
                => cf.CalculateRotationDisplacement()) * (_distanceFromLeftSide - _currentLength) / 100;
        }

        private void CalculateDeflectionFromVerticalDisplacements(ISpan span)
        {
            _spanDeflection += span.LeftNode.ConcentratedForces.Sum(cf 
                => cf.CalculateVerticalDisplacement()) / 100000;
        }

        private void CalculateDeflectionFromMomentForces(ISpan span)
        {
            _spanDeflection += (span.LeftNode.BendingMoment?.Value
                * (_distanceFromLeftSide - _currentLength)
                * (_distanceFromLeftSide - _currentLength) / 2 ?? 0)
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
            _spanDeflection += span.LeftNode.ConcentratedForces.Sum(cf => cf.CalculateBendingMoment(0)) *
                (_distanceFromLeftSide - _currentLength) *
                (_distanceFromLeftSide - _currentLength) / 2
                / (span.Material.YoungModulus * span.Section.MomentOfInteria);
        }


        private double GetForceAtTheCalculatedPoint(IContinousLoad load)
            => (load.EndPosition.Value - load.StartPosition.Value) /
                (load.EndPosition.Position - load.StartPosition.Position) *
                (_distanceFromLeftSide - _currentLength - load.StartPosition.Position) +
                load.StartPosition.Value;
    }

    #endregion // Private_Methods
}
