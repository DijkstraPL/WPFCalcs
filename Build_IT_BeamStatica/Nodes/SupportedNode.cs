using Build_IT_BeamStatica.Results.Displacements;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Results.Reactions;

namespace Build_IT_BeamStatica.Nodes
{
    internal class SupportedNode : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 1;

        #endregion // Properties

        #region Constructors
        
        public SupportedNode(
            IResultValue normalForce = null, 
            IResultValue shearForce = null, 
            IResultValue rotation = null)
        {
            NormalForce = normalForce ?? new NormalForce();
            ShearForce = shearForce ?? new ShearForce();
            LeftRotation = rotation ?? new Rotation();
            RightRotation = LeftRotation;
        }

        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = LeftRotationNumber;
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            VerticalMovementNumber = currentCounter++;
        }

        #endregion // Public_Methods
    }
}
