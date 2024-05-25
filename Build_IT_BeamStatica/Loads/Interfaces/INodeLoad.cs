namespace Build_IT_BeamStatica.Loads.Interfaces
{
    public interface INodeLoad : ILoad
    {
        #region Properties

        bool IncludeInSpanLoadCalculations { get; }

        #endregion // Properties

        #region Public_Methods

        double CalculateNormalForce();
        double CalculateShear();
        double CalculateBendingMoment(double distanceFromLoad);

        double CalculateHorizontalDisplacement();
        double CalculateVerticalDisplacement();
        double CalculateRotationDisplacement();

        double CalculateJointLoadVectorNormalForceMember();
        double CalculateJointLoadVectorShearMember();
        double CalculateJointLoadVectorBendingMomentMember();

        #endregion // Public_Methods
    }
}
