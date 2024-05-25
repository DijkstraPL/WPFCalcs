using Build_IT_BeamStatica.Builders.Nodes.Interfaces;
using Build_IT_BeamStatica.Nodes.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.Nodes
{
    public class SupportedNodeBuilder : NodeBuilder<SupportedNodeBuilder>
    {
        #region Constructors

        public SupportedNodeBuilder()
        {
            _nodeData.NodeType = NodeType.SupportedNode;
        }

        #endregion // Constructors
    }
}
