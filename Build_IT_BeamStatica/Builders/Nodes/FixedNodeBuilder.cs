using Build_IT_BeamStatica.Builders.Nodes.Interfaces;
using Build_IT_BeamStatica.Nodes.Enums;

namespace Build_IT_BeamStatica.Builders.Nodes
{
    public class FixedNodeBuilder : NodeBuilder<FixedNodeBuilder>
    {
        #region Constructors

        public FixedNodeBuilder()
        {
            _nodeData.NodeType = NodeType.FixedNode;
        }

        #endregion // Constructors
    }
}
