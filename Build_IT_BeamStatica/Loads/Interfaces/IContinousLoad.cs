using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.Interfaces
{
    public interface IContinousLoad
    {
        #region Properties

        ILoadWithPosition StartPosition { get; }
        ILoadWithPosition EndPosition { get; }
        double Length { get; }

        #endregion // Properties

        #region Public_Methods

        double CalculateNormalForce(double distanceFromLoadStartPosition);
        double CalculateShear(double distanceFromLoadStartPosition);
        double CalculateBendingMoment(double distanceFromLoadStartPosition);
        double CalculateRotation(ISpan span, double distanceFromLeftSide, double currentLength);
        double CalculateHorizontalDeflection(ISpan span, double distanceFromLeftSide, double currentLength);
        double CalculateVerticalDeflection(ISpan span, double distanceFromLeftSide, double currentLength);

        double CalculateSpanLoadVectorNormalForceMember(ISpan span, bool leftNode);
        double CalculateSpanLoadVectorShearMember(ISpan span, bool leftNode);
        double CalculateSpanLoadVectorBendingMomentMember(ISpan span, bool leftNode);

        #endregion // Public_Methods
    }
}
