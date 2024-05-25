using Build_IT_BeamStatica.Beams.Interfaces;
using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Beams.Interfaces;
using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans;
using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans.Interfaces;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Build_IT_CommonTools.MatrixMath.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Beams
{
    internal class DirectStiffnessCalculationEngine : IDirectStiffnessCalculationEngine
    {
        #region Fields

        private readonly IBeam _beam;
        private readonly IGlobalStiffnessMatrix _globalStiffnessMatrix;

        private IList<(ISpan span, ISpanCalculationEngine calculationEngine)> _spanCalculationEngines
            = new List<(ISpan, ISpanCalculationEngine)>();

        private VectorAdapter _jointLoadVector;
        private VectorAdapter _spanLoadVector;
        private VectorAdapter _deflectionVector;

        #endregion // Fields

        #region Constructor

        public DirectStiffnessCalculationEngine(IBeam beam, IGlobalStiffnessMatrix globalStiffnessMatrix = null)
        {
            _beam = beam ?? throw new ArgumentNullException(nameof(beam));
            _globalStiffnessMatrix = globalStiffnessMatrix ?? new GlobalStiffnessMatrix(_beam, _spanCalculationEngines);

            SetSpanCalculationEngines();
        }

        #endregion // Constructor

        #region Public_Methods

        public void Calculate()
        {
            _beam.SetNumeration();
            CalculateStiffnessMatrixes();
            _globalStiffnessMatrix.Calculate();
            if (_beam.IncludeSelfWeight)
                AddSelfWeightLoad();
            CalculateJointLoadVector();
            CalculateSpanLoadVectors();
            CalculateSpanLoadVector();
            CalculateDeflectionVector();
            if (CheckDeflectionVector())
                throw new ArgumentException("Mechanism");
            CalculateDisplacements();
            CalculateForces();
            CalculateReactions();
            SetAngledForces();
            AddForcesLocatedAtSupports();
        }

        #endregion // Public_Methods

        #region Private_Methods

        private void SetSpanCalculationEngines()
        {
            foreach (var span in _beam.Spans)
                _spanCalculationEngines.Add((span, new SpanCalculationEngine(span)));
        }

        private void AddSelfWeightLoad()
        {
            foreach (var span in _beam.Spans)
                span.IncludeSelfWeight = true;
        }

        private void CalculateStiffnessMatrixes()
        {
            foreach (var spanEnginePair in _spanCalculationEngines)
                spanEnginePair.calculationEngine.StiffnessMatrix.Calculate();
        }

        private void CalculateJointLoadVector()
        {
            if (_beam.NumberOfDegreesOfFreedom != 0)
                _jointLoadVector = VectorAdapter.Create(_beam.NumberOfDegreesOfFreedom);

            for (int i = 0; i < _beam.NumberOfDegreesOfFreedom; i++)
            {
                if (_beam.Nodes.Any(n => n.HorizontalMovementNumber == i))
                    _jointLoadVector[i] = _beam.Nodes.SingleOrDefault(n => n.HorizontalMovementNumber == i)?
                       .ConcentratedForces.Sum(cf => cf.CalculateJointLoadVectorNormalForceMember()) ?? 0;
                else if (_beam.Nodes.Any(n => n.VerticalMovementNumber == i))
                    _jointLoadVector[i] = _beam.Nodes.SingleOrDefault(n => n.VerticalMovementNumber == i)?
                        .ConcentratedForces.Sum(cf => cf.CalculateJointLoadVectorShearMember()) ?? 0;
                else
                    _jointLoadVector[i] = _beam.Nodes.SingleOrDefault(n => n.LeftRotationNumber == i || n.RightRotationNumber == i)?
                        .ConcentratedForces.Sum(cf => cf.CalculateJointLoadVectorBendingMomentMember()) ?? 0;
            }
        }

        private void CalculateSpanLoadVectors()
        {
            foreach (var spanEnginePair in _spanCalculationEngines)
                spanEnginePair.calculationEngine.CalculateSpanLoadVector();
        }

        private void CalculateSpanLoadVector()
        {
            if (_beam.NumberOfDegreesOfFreedom != 0)
                _spanLoadVector = VectorAdapter.Create(_beam.NumberOfDegreesOfFreedom);
            foreach (var spanEnginePair in _spanCalculationEngines)
                CalculateSpanLoadVectorForCurrentSpan(spanEnginePair);
        }

        private void CalculateDeflectionVector()
        {
            if (_beam.NumberOfDegreesOfFreedom != 0)
                _deflectionVector = _globalStiffnessMatrix.InversedMatrix * (_jointLoadVector - _spanLoadVector);
        }

        private bool CheckDeflectionVector()
            => _deflectionVector != null && _deflectionVector.Any(dv => double.IsNaN(dv));

        private void CalculateDisplacements()
        {
            foreach (var spanEnginePair in _spanCalculationEngines)
                spanEnginePair.calculationEngine.CalculateDisplacement(_deflectionVector, _beam.NumberOfDegreesOfFreedom);
        }

        private void CalculateForces()
        {
            foreach (var spanEnginePair in _spanCalculationEngines)
                spanEnginePair.calculationEngine.CalculateForce(
                    spanEnginePair.calculationEngine.LoadVector,
                    spanEnginePair.calculationEngine.Displacements);
        }

        private void CalculateReactions()
        {
            int numberOfReactions = _beam.Spans.Count * 3 + 3 - _beam.NumberOfDegreesOfFreedom;

            numberOfReactions += _beam.Nodes.Count(n => n is Hinge);

            for (int i = _beam.NumberOfDegreesOfFreedom; i < numberOfReactions + _beam.NumberOfDegreesOfFreedom; i++)
            {
                SetLeftNodeReactions(i);
                SetRightNodeReactions(i);
            }
        }

        private void SetLeftNodeReactions(int i)
        {
            if (_beam.Spans.SingleOrDefault(s => s.LeftNode.HorizontalMovementNumber == i)?.LeftNode.NormalForce != null)
                _beam.Spans.SingleOrDefault(s => s.LeftNode.HorizontalMovementNumber == i).LeftNode.NormalForce.Value
                    += _spanCalculationEngines.Where(sep => sep.span.LeftNode.HorizontalMovementNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[0]);

            if (_beam.Spans.SingleOrDefault(s => s.LeftNode.VerticalMovementNumber == i)?.LeftNode.ShearForce != null)
                _beam.Spans.SingleOrDefault(s => s.LeftNode.VerticalMovementNumber == i).LeftNode.ShearForce.Value
                    += _spanCalculationEngines.Where(sep => sep.span.LeftNode.VerticalMovementNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[1]);

            if (_beam.Spans.SingleOrDefault(s => s.LeftNode.RightRotationNumber == i)?.LeftNode.BendingMoment != null)
                _beam.Spans.SingleOrDefault(s => s.LeftNode.RightRotationNumber == i).LeftNode.BendingMoment.Value
                    -= _spanCalculationEngines.Where(sep => sep.span.LeftNode.RightRotationNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[2]);
        }

        private void SetRightNodeReactions(int i)
        {
            if (_beam.Spans.SingleOrDefault(s => s.RightNode.HorizontalMovementNumber == i)?.RightNode.NormalForce != null)
                _beam.Spans.SingleOrDefault(s => s.RightNode.HorizontalMovementNumber == i).RightNode.NormalForce.Value
                    += _spanCalculationEngines.Where(sep => sep.span.RightNode.HorizontalMovementNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[3]);

            if (_beam.Spans.SingleOrDefault(s => s.RightNode.VerticalMovementNumber == i)?.RightNode.ShearForce != null)
                _beam.Spans.SingleOrDefault(s => s.RightNode.VerticalMovementNumber == i).RightNode.ShearForce.Value
                    += _spanCalculationEngines.Where(sep => sep.span.RightNode.VerticalMovementNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[4]);

            if (_beam.Spans.SingleOrDefault(s => s.RightNode.LeftRotationNumber == i)?.RightNode.BendingMoment != null)
                _beam.Spans.SingleOrDefault(s => s.RightNode.LeftRotationNumber == i).RightNode.BendingMoment.Value
                    -= _spanCalculationEngines.Where(sep => sep.span.RightNode.LeftRotationNumber == i)
                    .Sum(sep => sep.calculationEngine.Forces[5]);
        }

        private void SetAngledForces()
        {
            foreach (var node in _beam.Nodes)
                if (node.RadiansAngle != 0)
                    SetAngledForces(node);
        }

        private void SetAngledForces(INode node)
        {
            double normalForce = node.NormalForce?.Value ?? 0;
            double shearForce = node.ShearForce?.Value ?? 0;

            if (node.NormalForce != null)
                node.NormalForce.Value =
                    normalForce * Math.Cos(node.RadiansAngle) +
                    shearForce * Math.Sin(node.RadiansAngle);

            if (node.ShearForce != null)
                node.ShearForce.Value =
                    shearForce * Math.Cos(node.RadiansAngle) +
                    normalForce * Math.Sin(node.RadiansAngle);
        }

        private void AddForcesLocatedAtSupports()
        {
            foreach (var node in _beam.Nodes)
            {
                if (node.NormalForce != null)
                    node.NormalForce.Value -= node.ConcentratedForces.Sum(cf => cf.CalculateNormalForce());
                if (node.ShearForce != null)
                    node.ShearForce.Value -= node.ConcentratedForces.Sum(cf => cf.CalculateShear());
                if (node.BendingMoment != null)
                    node.BendingMoment.Value -= node.ConcentratedForces.Sum(cf => cf.CalculateBendingMoment(0));
            }
        }

        private void CalculateSpanLoadVectorForCurrentSpan
            ((ISpan span, ISpanCalculationEngine calculationEngine) spanEnginePair)
        {
            for (int i = 0; i < _beam.NumberOfDegreesOfFreedom; i++)
            {
                if (spanEnginePair.span.LeftNode.HorizontalMovementNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[0];
                else if (spanEnginePair.span.LeftNode.VerticalMovementNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[1];
                else if (spanEnginePair.span.LeftNode.RightRotationNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[2];
                else if (spanEnginePair.span.RightNode.HorizontalMovementNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[3];
                else if (spanEnginePair.span.RightNode.VerticalMovementNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[4];
                else if (spanEnginePair.span.RightNode.LeftRotationNumber == i)
                    _spanLoadVector[i] += spanEnginePair.calculationEngine.LoadVector[5];
            }
        }

        #endregion // Private_Methods
    }
}
