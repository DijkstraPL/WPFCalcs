using Build_IT_BeamStatica.Builders.Nodes.Interfaces;
using Build_IT_BeamStatica.Nodes.Enums;

namespace Build_IT_BeamStatica.Builders.Nodes
{
    public class HingeBuilder : NodeBuilder<HingeBuilder>
    {
        #region Constructors

        public HingeBuilder()
        {
            _nodeData.NodeType = NodeType.Hinge;
        }

        #endregion // Constructors
    }
}
