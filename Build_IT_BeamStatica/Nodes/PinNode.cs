using Build_IT_BeamStatica.Results.Displacements;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Results.Reactions;
using System;

namespace Build_IT_BeamStatica.Nodes
{
    internal class PinNode : Node
    {
        #region Properties

        public override short DegreesOfFreedom => 2;

        #endregion // Properties

        #region Constructors

        public PinNode(
            IResultValue shearForce = null,
            IResultValue horizontalDeflection = null,
            IResultValue rotation = null)
        {
            ShearForce = shearForce ?? new ShearForce();
            HorizontalDeflection = horizontalDeflection ?? new HorizontalDeflection();
            LeftRotation = rotation ?? new Rotation();
            RightRotation = LeftRotation;
        }

        public PinNode(double angle,
            IResultValue shearForce = null,
            IResultValue horizontalDeflection = null,
            IResultValue rotation = null,
            IResultValue normalForce = null,
            IResultValue verticalDeflection = null
            ) : this(shearForce, horizontalDeflection, rotation)
        {
            Angle = angle % 360;
            //if(Angle = 90 || Angle == -90)
            //{
            //    NormalForce = ShearForce;
            //}
            if (Angle != 0 && Angle != 180 && Angle != -180)
            {
                NormalForce = normalForce ?? new NormalForce();
                VerticalDeflection = verticalDeflection ?? new VerticalDeflection();
            }
        }

        #endregion // Constructors

        #region Public_Methods

        public override void SetDisplacementNumeration(ref short currentCounter)
        {
            HorizontalMovementNumber = currentCounter++;
            LeftRotationNumber = currentCounter++;
            RightRotationNumber = LeftRotationNumber;
        }

        public override void SetReactionNumeration(ref short currentCounter)
        {
            VerticalMovementNumber = currentCounter++;
        }

        #endregion // Public_Methods
    }
}
