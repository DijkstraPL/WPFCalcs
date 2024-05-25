using Build_IT_BeamStatica.Builders.Nodes.Interfaces;
using Build_IT_BeamStatica.Nodes.Enums;

namespace Build_IT_BeamStatica.Builders.Nodes
{
    public class FreeNodeBuilder : NodeBuilder<FreeNodeBuilder>
    {
        #region Constructors

        public FreeNodeBuilder()
        {
            _nodeData.NodeType = NodeType.FreeNode;
        }

        #endregion // Constructors
    }
}
