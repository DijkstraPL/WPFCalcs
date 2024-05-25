using Build_IT_BeamStatica.Nodes.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Data
{
    public class NodeData
    {
        public double Angle { get; set; }
        public NodeType NodeType { get; set; }
        public IList<PointLoadData> PointLoads { get; private set; } = new List<PointLoadData>();
    }
}
