using Build_IT_BeamStatica.Nodes.Interfaces;

namespace Build_IT_BeamStatica.Spans.Interfaces
{
    public interface INodesProvider
    {
        #region Properties

        INode LeftNode { get; }
        INode RightNode { get; }

        #endregion // Properties
    }
}
