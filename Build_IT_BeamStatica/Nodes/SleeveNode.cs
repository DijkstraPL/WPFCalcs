using Build_IT_BeamStatica.Results.Displacements;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Results.Reactions;

namespace Build_IT_BeamStatica.Nodes
{
    internal class SleeveNode : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 1;

        #endregion // Properties

        #region Constructors

        public SleeveNode(
            IResultValue shearForce = null, 
            IResultValue bendingMoment = null, 
            IResultValue horizontalDeflection = null)
        {
            ShearForce = shearForce ?? new ShearForce();
            BendingMoment = bendingMoment ?? new BendingMoment();
            HorizontalDeflection = horizontalDeflection ?? new HorizontalDeflection ();
        }

        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
            VerticalMovementNumber = currentCounter++;
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = LeftRotationNumber;
        }

        #endregion // Public_Methods
    }
}
