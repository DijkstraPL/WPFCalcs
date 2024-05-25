using Build_IT_BeamStatica.Nodes.Enums;
using System.Collections.Generic;

namespace Build_IT_BeamStatica.Data
{
    public class SpanData
    {
        #region Properties
        
        public double Length { get; set; }
        public MaterialData Material { get; set; }
        public SectionData Section { get; set; }
        public NodeData LeftNode { get; set; }
        public NodeData RightNode { get; set; }
        public bool IncludeSelfWeight { get; set; }

        public IList<ContinuousLoadData> ContinuousLoads { get; private set; } = new List<ContinuousLoadData>();
        public IList<PointLoadData> PointLoads { get; private set; } = new List<PointLoadData>();

        #endregion // Properties
    }
}
