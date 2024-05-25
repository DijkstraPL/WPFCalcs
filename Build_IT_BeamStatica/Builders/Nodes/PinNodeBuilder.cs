using Build_IT_BeamStatica.Builders.Nodes.Interfaces;
using Build_IT_BeamStatica.Builders.PointLoads;
using Build_IT_BeamStatica.Builders.PointLoads.Interfaces;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Nodes.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.Nodes
{
    public class PinNodeBuilder : NodeBuilder<PinNodeBuilder>
    {
        #region Constructors

        public PinNodeBuilder()
        {
            _nodeData.NodeType = NodeType.PinNode;
        }

        #endregion // Constructors

        #region Public_Methods
        
        public PinNodeBuilder With(double angle)
        {
            _nodeData.Angle = angle;
            return this;
        }

        #endregion // Public_Methods
    }
}
