using Build_IT_BeamStatica.Results.Displacements;
using Build_IT_BeamStatica.Results.Interfaces;

namespace Build_IT_BeamStatica.Nodes
{
    internal class FreeNode : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 3;

        #endregion // Properties

        #region Constructors 

        public FreeNode(
            IResultValue horizontalDeflection = null, 
            IResultValue verticalDeflection = null,
            IResultValue rotation = null)
        {
            HorizontalDeflection = horizontalDeflection ?? new HorizontalDeflection();
            VerticalDeflection = verticalDeflection ?? new VerticalDeflection();
            LeftRotation = rotation ?? new Rotation();
            RightRotation = LeftRotation;
        }

        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            VerticalMovementNumber = currentCounter++;
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = LeftRotationNumber;
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
        }

        #endregion // Public_Methods
    }
}
