using Build_IT_DataAccess.SteelProfiles.Entities.Enums;

namespace Build_IT_DataAccess.SteelProfiles.Entities
{
    public class SectionPoint
    {
        public long Id { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public ChamferType ChamferType { get; set; }
        public string ChamferX { get; set; }
        public string ChamferY { get; set; }
    }
}
