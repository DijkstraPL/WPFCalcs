using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_DataAccess.SteelProfiles.Entities
{
    public class SteelProfile
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<ParameterValue> ParametersValues { get; private set; }

        public SteelProfile()
        {
            ParametersValues = new HashSet<ParameterValue>();
        }
    }
}
