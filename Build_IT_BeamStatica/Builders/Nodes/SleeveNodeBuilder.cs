using Build_IT_BeamStatica.Builders.Nodes.Interfaces;
using Build_IT_BeamStatica.Nodes.Enums;

namespace Build_IT_BeamStatica.Builders.Nodes
{
    public class SleeveNodeBuilder : NodeBuilder<SleeveNodeBuilder>
    {
        #region Constructors

        public SleeveNodeBuilder()
        {
            _nodeData.NodeType = NodeType.SleeveNode;
        }

        #endregion // Constructors
    }
}
