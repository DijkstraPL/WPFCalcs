using Build_IT_Data.Geometry.Enums;

namespace Build_IT_DataAccess.Sections.Entities
{
    public class ContourPoint : Point
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int OrderNumber { get; set; }
        public PointType PointType { get; set; }
        public double Value { get; set; }
    }
}
