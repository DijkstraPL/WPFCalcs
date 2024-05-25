using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_CommonTools.Attributes;

namespace Build_IT_BeamStatica.Spans.Interfaces
{
    public interface ISpanData
    {
        #region Properties

        INode LeftNode { get; }
        [Abbreviation("L")]
        [Unit("m")]
        double Length { get; }
        INode RightNode { get; }
        MaterialData Material { get; }
        SectionData Section { get; }

        #endregion // Properties
    }
}
