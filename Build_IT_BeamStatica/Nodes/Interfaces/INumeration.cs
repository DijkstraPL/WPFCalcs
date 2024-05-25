namespace Build_IT_BeamStatica.Nodes.Interfaces
{
    public interface INumeration
    {
        #region Properties

        short HorizontalMovementNumber { get; }
        short VerticalMovementNumber { get; }
        short LeftRotationNumber { get; }
        short RightRotationNumber { get; }

        #endregion // Properties

        #region Public_Methods

        void SetDisplacementNumeration(ref short currentCounter);
        void SetReactionNumeration(ref short currentCounter);

        #endregion // Public_Methods
    }
}
