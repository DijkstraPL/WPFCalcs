using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_BeamStatica.Results.Interfaces;
using System;
using System.Collections.Generic;

namespace Build_IT_BeamStatica.Nodes
{
    internal abstract class Node : INode
    {
        #region Properties

        public double Angle { get; protected set; }
        public double RadiansAngle => Angle * Math.PI / 180;

        public short HorizontalMovementNumber { get; protected set; }
        public short VerticalMovementNumber { get; protected set; }
        public short LeftRotationNumber { get; protected set; }
        public short RightRotationNumber { get; protected set; }

        public abstract short DegreesOfFreedom { get; }

        public ICollection<INodeLoad> ConcentratedForces { get; set; } = new List<INodeLoad>();

        public IResultValue NormalForce { get; protected set; } = null;
        public IResultValue ShearForce { get; protected set; } = null;
        public IResultValue BendingMoment { get; protected set; } = null;

        public IResultValue HorizontalDeflection { get; protected set; } = null;
        public IResultValue VerticalDeflection { get; protected set; } = null;
        public IResultValue LeftRotation { get; protected set; } = null;
        public IResultValue RightRotation { get; protected set; } = null;

        #endregion // Properties

        #region Public_Methods

        public abstract void SetDisplacementNumeration(ref short currentCounter);
        public abstract void SetReactionNumeration(ref short currentCounter);

        #endregion // Public_Methods
    }
}
