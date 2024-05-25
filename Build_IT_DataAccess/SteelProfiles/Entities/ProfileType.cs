using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_DataAccess.SteelProfiles.Entities
{
    public class ProfileType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Parameter> Parameters { get; private set; }
        public ICollection<SectionPoint> SectionPoints { get; private set; }
        public ICollection<SteelProfile> SteelProfiles { get; private set; }

        public ProfileType()
        {
            Parameters = new HashSet<Parameter>();
            SectionPoints = new HashSet<SectionPoint>();
            SteelProfiles = new HashSet<SteelProfile>();
        }
    }
}
