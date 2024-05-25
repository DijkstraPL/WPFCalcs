using Build_IT_BeamStatica.Builders.Nodes.Interfaces;
using Build_IT_BeamStatica.Nodes.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.Nodes
{
   public  class SupportedNodeWithHingeBuilder : NodeBuilder<SupportedNodeWithHingeBuilder>
    {
        #region Constructors

        public SupportedNodeWithHingeBuilder()
        {
            _nodeData.NodeType = NodeType.SupportedNodeWithHinge;
        }

        #endregion // Constructors
    }
}
