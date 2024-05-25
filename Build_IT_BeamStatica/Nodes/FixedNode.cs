using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Results.Reactions;

namespace Build_IT_BeamStatica.Nodes
{
    internal class FixedNode : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 0;

        #endregion // Properties

        #region Constructors

        public FixedNode(
            IResultValue normalForce = null,
            IResultValue shearForce = null, 
            IResultValue bendingMoment = null)
        {
            NormalForce = normalForce ?? new NormalForce();
            ShearForce = shearForce ?? new ShearForce();
            BendingMoment = bendingMoment ?? new BendingMoment();
        }

        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {           
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            VerticalMovementNumber = currentCounter++;
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = LeftRotationNumber;
        }

        #endregion // Public_Methods
    }
}
