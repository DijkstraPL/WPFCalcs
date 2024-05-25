using Build_IT_BeamStatica.Data;

namespace Build_IT_BeamStatica.Spans.Interfaces
{
    public interface ISpan : ILengthProvider, INodesProvider, ILoadProvider
    {
        #region Properties

        short Number { get; set; }
        SectionData Section { get; }
        MaterialData Material { get; }

        bool IncludeSelfWeight { get; set; }

        #endregion // Properties
    }
}
