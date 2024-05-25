using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans.Interfaces;
using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Build_IT_CommonTools.MatrixMath.Wrappers;
using System;
using System.Linq;

namespace Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans
{
    internal class SpanCalculationEngine : ISpanCalculationEngine
    {
        #region Properties

        public IStiffnessMatrix StiffnessMatrix { get; }

        public VectorAdapter LoadVector { get; private set; }
        public VectorAdapter Displacements { get; private set; }
        public VectorAdapter Forces { get; private set; }

        #endregion // Properties

        #region Fields

        private const double g = 9.812;

        private readonly ISpan _span;

        #endregion // Fields

        public SpanCalculationEngine(ISpan span, IStiffnessMatrix stiffnessMatrix = null)
        {
            _span = span ?? throw new ArgumentNullException(nameof(span));
            StiffnessMatrix = stiffnessMatrix ?? new StiffnessMatrix(_span);
        }

        #region Public_Methods

        public void CalculateSpanLoadVector()
        {
            if (_span.IncludeSelfWeight)
                AddSelfWeightLoad();

            LoadVector = VectorAdapter.Create(StiffnessMatrix.Size);

            LoadVector[0] = SetNormalForceLoadVector(isLeftNode: true);
            LoadVector[1] = SetShearForceLoadVector(isLeftNode: true);
            LoadVector[2] = SetBendingMomentForceLoadVector(isLeftNode: true);

            LoadVector[3] = SetNormalForceLoadVector(isLeftNode: false);
            LoadVector[4] = SetShearForceLoadVector(isLeftNode: false);
            LoadVector[5] = SetBendingMomentForceLoadVector(isLeftNode: false);

            if (_span.LeftNode.Angle != 0)
                SetLoadVectorsWithAngle(isLeftNode: true);
            if (_span.RightNode.Angle != 0)
                SetLoadVectorsWithAngle(isLeftNode: false);
        }

        public void CalculateDisplacement(VectorAdapter deflectionVector, int numberOfDegreesOfFreedom)
        {
            CalculateDisplacementVector(deflectionVector, numberOfDegreesOfFreedom);
            SetDisplacement();
        }

        public void CalculateForce(VectorAdapter loadVector, VectorAdapter displacements)
        {
            Forces = StiffnessMatrix.Matrix.Multiply(displacements).Add(loadVector);
        }

        #endregion // Public_Methods

        #region Private_Methods

        private void AddSelfWeightLoad()
        {
            double load = -_span.Material.Density * g / 1000 * _span.Section.Area / 10000;
            _span.ContinousLoads.Add(ContinuousShearLoad.Create(0, load, _span.Length, load));
        }

        private double SetNormalForceLoadVector(bool isLeftNode)
            => _span.ContinousLoads.Sum(cl => cl.CalculateSpanLoadVectorNormalForceMember(_span, isLeftNode)) +
                _span.PointLoads.Sum(pl => pl.CalculateSpanLoadVectorNormalForceMember(_span, isLeftNode)) +
                _span.LeftNode.ConcentratedForces.Where(cf => cf.IncludeInSpanLoadCalculations)
                .Sum(cf => (cf as ISpanLoad)?.CalculateSpanLoadVectorNormalForceMember(_span, isLeftNode) ?? 0) +
               _span.RightNode.ConcentratedForces.Where(cf => cf.IncludeInSpanLoadCalculations)
                .Sum(cf => (cf as ISpanLoad)?.CalculateSpanLoadVectorNormalForceMember(_span, isLeftNode) ?? 0);

        private double SetShearForceLoadVector(bool isLeftNode)
            => _span.ContinousLoads.Sum(cl => cl.CalculateSpanLoadVectorShearMember(_span, isLeftNode)) +
                _span.PointLoads.Sum(pl => pl.CalculateSpanLoadVectorShearMember(_span, isLeftNode)) +
                _span.LeftNode.ConcentratedForces.Where(cf => cf.IncludeInSpanLoadCalculations)
                .Sum(cf => (cf as ISpanLoad)?.CalculateSpanLoadVectorShearMember(_span, isLeftNode) ?? 0) +
                _span.RightNode.ConcentratedForces.Where(cf => cf.IncludeInSpanLoadCalculations)
                .Sum(cf => (cf as ISpanLoad)?.CalculateSpanLoadVectorShearMember(_span, isLeftNode) ?? 0);

        private double SetBendingMomentForceLoadVector(bool isLeftNode)
            => _span.ContinousLoads.Sum(cl => cl.CalculateSpanLoadVectorBendingMomentMember(_span, isLeftNode)) +
                _span.PointLoads.Sum(pl => pl.CalculateSpanLoadBendingMomentMember(_span, isLeftNode)) +
                _span.LeftNode.ConcentratedForces.Where(cf => cf.IncludeInSpanLoadCalculations)
                .Sum(cf => (cf as ISpanLoad)?.CalculateSpanLoadBendingMomentMember(_span, isLeftNode) ?? 0) +
                _span.RightNode.ConcentratedForces.Where(cf => cf.IncludeInSpanLoadCalculations)
                .Sum(cf => (cf as ISpanLoad)?.CalculateSpanLoadBendingMomentMember(_span, isLeftNode) ?? 0);

        private void SetLoadVectorsWithAngle(bool isLeftNode)
        {
            int index = isLeftNode ? 0 : 3;
            double angle = isLeftNode ? _span.LeftNode.RadiansAngle : _span.RightNode.RadiansAngle;

            double normalForceLoadVector = LoadVector[index];
            double shearForceLoadVector = LoadVector[index + 1];

            LoadVector[index] =
                normalForceLoadVector * Math.Cos(angle) -
                shearForceLoadVector * Math.Sin(angle);
            LoadVector[index + 1] =
                shearForceLoadVector * Math.Cos(angle) +
                normalForceLoadVector * Math.Sin(angle);
        }

        private void SetDisplacement()
        {
            if (_span.LeftNode.HorizontalDeflection != null)
                _span.LeftNode.HorizontalDeflection.Value = Displacements[0] * 1000; // mm
            if (_span.LeftNode.VerticalDeflection != null)
                _span.LeftNode.VerticalDeflection.Value = Displacements[1] * 1000; // mm
            if (_span.LeftNode.RightRotation != null)
                _span.LeftNode.RightRotation.Value = Displacements[2];
            if (_span.RightNode.HorizontalDeflection != null)
                _span.RightNode.HorizontalDeflection.Value = Displacements[3] * 1000; // mm
            if (_span.RightNode.VerticalDeflection != null)
                _span.RightNode.VerticalDeflection.Value = Displacements[4] * 1000; // mm
            if (_span.RightNode.LeftRotation != null)
                _span.RightNode.LeftRotation.Value = Displacements[5];

            if (_span.LeftNode.RadiansAngle != 0)
                SetDisplacementsInAngledSupport(_span.LeftNode);
            if (_span.RightNode.RadiansAngle != 0)
                SetDisplacementsInAngledSupport(_span.RightNode);
        }

        private void CalculateDisplacementVector(VectorAdapter deflectionVector, int numberOfDegreesOfFreedom)
        {
            Displacements = VectorAdapter.Create(StiffnessMatrix.Size);

            if (_span.LeftNode.HorizontalMovementNumber < numberOfDegreesOfFreedom)
                Displacements[0] = deflectionVector[_span.LeftNode.HorizontalMovementNumber];
            if (_span.LeftNode.VerticalMovementNumber < numberOfDegreesOfFreedom)
                Displacements[1] = deflectionVector[_span.LeftNode.VerticalMovementNumber];
            if (_span.LeftNode.RightRotationNumber < numberOfDegreesOfFreedom)
                Displacements[2] = deflectionVector[_span.LeftNode.RightRotationNumber];
            if (_span.RightNode.HorizontalMovementNumber < numberOfDegreesOfFreedom)
                Displacements[3] = deflectionVector[_span.RightNode.HorizontalMovementNumber];
            if (_span.RightNode.VerticalMovementNumber < numberOfDegreesOfFreedom)
                Displacements[4] = deflectionVector[_span.RightNode.VerticalMovementNumber];
            if (_span.RightNode.LeftRotationNumber < numberOfDegreesOfFreedom)
                Displacements[5] = deflectionVector[_span.RightNode.LeftRotationNumber];
        }

        private void SetDisplacementsInAngledSupport(INode node)
        {
            double horizontalDeflection =  node.HorizontalDeflection?.Value ?? 0;
            double verticalDeflection = node.VerticalDeflection?.Value ?? 0;

            if (node.VerticalDeflection != null)
                node.VerticalDeflection.Value =
                    verticalDeflection * Math.Cos(node.RadiansAngle) -
                    horizontalDeflection * Math.Sin(node.RadiansAngle) ;
            if (node.HorizontalDeflection != null)
                node.HorizontalDeflection.Value =
                   horizontalDeflection * Math.Cos(node.RadiansAngle) -
                     verticalDeflection * Math.Sin(node.RadiansAngle);
        }
        #endregion // Private_Methods
    }
}
