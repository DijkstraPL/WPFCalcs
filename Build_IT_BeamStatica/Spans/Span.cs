using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Build_IT_CommonTools.Attributes;
using System;
using System.Collections.Generic;

namespace Build_IT_BeamStatica.Spans
{
    internal class Span : ISpan
    {
        #region Properties

        public short Number { get; set; }

        public INode LeftNode { get; }
        [Abbreviation("L")]
        [Unit("m")]
        public double Length { get; }
        public INode RightNode { get; }
        public MaterialData Material { get; }
        public SectionData Section { get; }

        public ICollection<IContinousLoad> ContinousLoads { get; private set; }
        public ICollection<ISpanLoad> PointLoads { get; private set; }
        
        public bool IncludeSelfWeight { get; set; }

        #endregion // Properties

        #region Constructors

        public Span(INode leftNode, double length, INode rightNode,
            MaterialData material, SectionData section, bool includeSelfWeight)
        {
            LeftNode = leftNode ?? throw new ArgumentNullException(nameof(leftNode));
            Length = length > 0 ? length : throw new ArgumentOutOfRangeException(nameof(length));
            RightNode = rightNode ?? throw new ArgumentNullException(nameof(rightNode));
            Material = material ?? throw new ArgumentNullException(nameof(material));
            Section = section ?? throw new ArgumentNullException(nameof(section));

            ContinousLoads = new List<IContinousLoad>();
            PointLoads = new List<ISpanLoad>();

            IncludeSelfWeight = includeSelfWeight;
        }

        public Span(ISpanData spanData, bool includeSelfWeight)
            : this(spanData.LeftNode, spanData.Length, spanData.RightNode,
                  spanData.Material, spanData.Section, includeSelfWeight)
        {
        }
        
        #endregion // Constructors
    }
}