using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_BeamStatica.Spans;
using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Factories
{
    internal static class SpanFactory
    {
        #region Public_Methods

        public static ISpan Create(INode leftNode, double length, INode rightNode,
            MaterialData material, SectionData section, bool includeSelfWeight = true)
        {
            return new Span(leftNode, length, rightNode, material, section, includeSelfWeight);
        }

        #endregion // Public_Methods
    }
}
