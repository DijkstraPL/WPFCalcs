using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Data
{
    public class BeamData
    {
        public bool IncludeSelfWeight { get; set; }
        public IList<SpanData> SpanDatas { get; private set; } = new List<SpanData>();
    }
}
