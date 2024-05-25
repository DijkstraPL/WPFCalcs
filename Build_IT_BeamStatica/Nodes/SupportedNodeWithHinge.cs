using Build_IT_BeamStatica.Results.Displacements;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Results.Reactions;

namespace Build_IT_BeamStatica.Nodes
{
    internal class SupportedNodeWithHinge : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 2;

        #endregion // Properties

        #region Constructors

        public SupportedNodeWithHinge(
            IResultValue normalForce = null, 
            IResultValue shearForce = null, 
            IResultValue leftRotation = null, 
            IResultValue rightRotation = null)
        {
            NormalForce = normalForce ?? new NormalForce();
            ShearForce = shearForce ?? new ShearForce();
            LeftRotation = leftRotation ?? new Rotation();
            RightRotation = rightRotation ?? new Rotation();
        }

        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = currentCounter++;
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            VerticalMovementNumber = currentCounter++;
        }

        #endregion // Public_Methods
    }
}
