using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_DataAccess.SteelProfiles.Entities
{
    public class ParameterValue
    {
        public long Id { get; set; }
        public Parameter Parameter { get; set; }
        public long ParameterId { get; set; }
        public string Value { get; set; }
    }
}
