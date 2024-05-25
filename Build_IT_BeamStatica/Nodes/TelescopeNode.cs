using Build_IT_BeamStatica.Results.Displacements;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Results.Reactions;

namespace Build_IT_BeamStatica.Nodes
{
    internal class TelescopeNode : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 1;

        #endregion // Properties

        #region Constructors
        
        public TelescopeNode(
            IResultValue normalForce = null, 
            IResultValue bendingMoment = null, 
            IResultValue verticalDeflection = null)
        {
            NormalForce = normalForce ?? new NormalForce();
            BendingMoment = bendingMoment ?? new BendingMoment();
            VerticalDeflection = verticalDeflection ?? new VerticalDeflection();
        }

        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {
            VerticalMovementNumber = currentCounter++;
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = LeftRotationNumber;
        }

        #endregion // Public_Methods
    }
}
