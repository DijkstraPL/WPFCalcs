using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Nodes.Enums;
using Build_IT_BeamStatica.Nodes.Interfaces;
using System;

namespace Build_IT_BeamStatica.Factories
{
    internal static class NodeFactory
    {
        #region Public_Methods
        
        public static INode Create(NodeData nodeData, double angle = 0)
        {
            switch (nodeData.NodeType)
            {
                case NodeType.FixedNode:
                    return new FixedNode();
                case NodeType.FreeNode:
                    return new FreeNode();
                case NodeType.Hinge:
                    return new Hinge();
                case NodeType.PinNode:
                    return new PinNode(angle);
                case NodeType.SleeveNode:
                    return new SleeveNode();
                case NodeType.SupportedNode:
                    return new SupportedNode();
                case NodeType.SupportedNodeWithHinge:
                    return new SupportedNodeWithHinge();
                case NodeType.TelescopeNode:
                    return new TelescopeNode();
            }
            throw new NotSupportedException();
        }

        #endregion // Public_Methods
    }
}
