using Build_IT_BeamStatica.Builders.Nodes.Interfaces;
using Build_IT_BeamStatica.Nodes.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.Nodes
{
    public class TelescopeNodeBuilder : NodeBuilder<TelescopeNodeBuilder>
    {
        #region Constructors

        public TelescopeNodeBuilder()
        {
            _nodeData.NodeType = NodeType.TelescopeNode;
        }

        #endregion // Constructors
    }
}
