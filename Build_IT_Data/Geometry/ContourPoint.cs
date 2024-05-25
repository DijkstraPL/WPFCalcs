using Build_IT_Data.Geometry.Enums;

namespace Build_IT_Data.Geometry
{
    public class ContourPoint : Point
    {
        public PointType PointType { get; set; }
        public double Value { get; set; }

        public ContourPoint(double x, double y, PointType pointType = PointType.None, double value = 0) : base(x, y)
        {
            PointType = pointType;
            Value = value;
        }
    }
}
