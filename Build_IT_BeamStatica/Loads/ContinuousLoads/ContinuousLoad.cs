using Build_IT_BeamStatica.Loads.ContinuousLoads.Interfaces;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using System;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads
{
    internal abstract class ContinuousLoad : IContinousLoad
    {
        #region Properties

        public ILoadWithPosition StartPosition { get; }
        public ILoadWithPosition EndPosition { get; }
        public double Length => EndPosition.Position - StartPosition.Position;

        protected IForceResult NormalForceResult { get; set; }
        protected IForceResult ShearResult { get; set; }
        protected IForceResult BendingMomentResult { get; set; }

        protected IDisplacementResult HorizontalDeflectionResult { get; set; }
        protected IDisplacementResult VerticalDeflectionResult { get; set; }
        protected IDisplacementResult RotationResult { get; set; }

        #endregion // Properties

        #region Constructors

        protected ContinuousLoad(ILoadWithPosition startPosition, ILoadWithPosition endPosition)
        {
            StartPosition = startPosition ?? throw new ArgumentNullException(nameof(startPosition));
            EndPosition = endPosition ?? throw new ArgumentNullException(nameof(endPosition));
        }

        #endregion // Constructors

        #region Public_Methods

        public virtual double CalculateNormalForce(double distanceFromLoadStartPosition)
            => NormalForceResult?.GetValue(distanceFromLoadStartPosition) ?? 0;

        public virtual double CalculateShear(double distanceFromLoadStartPosition)
            => ShearResult?.GetValue(distanceFromLoadStartPosition) ?? 0;

        public virtual double CalculateBendingMoment(double distanceFromLoadStartPosition)
            => BendingMomentResult?.GetValue(distanceFromLoadStartPosition) ?? 0;

        public virtual double CalculateRotation(ISpan span, double distanceFromLeftSide, double currentLength)
            => RotationResult?.GetValue(span, distanceFromLeftSide, currentLength) ?? 0;

        public virtual double CalculateVerticalDeflection(ISpan span, double distanceFromLeftSide, double currentLength)
            => VerticalDeflectionResult?.GetValue(span, distanceFromLeftSide, currentLength) ?? 0;

        public virtual double CalculateHorizontalDeflection(ISpan span, double distanceFromLeftSide, double currentLength) 
            => HorizontalDeflectionResult?.GetValue(span, distanceFromLeftSide, currentLength) ?? 0;

        public virtual double CalculateSpanLoadVectorNormalForceMember(ISpan span, bool leftNode) => 0;
        public virtual double CalculateSpanLoadVectorShearMember(ISpan span, bool leftNode) => 0;
        public virtual double CalculateSpanLoadVectorBendingMomentMember(ISpan span, bool leftNode) => 0;
        
        #endregion // Public_Methods
    }
}
