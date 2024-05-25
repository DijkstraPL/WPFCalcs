namespace Build_IT_DataAccess.SnowLoads.Entities
{
    public class MultiSpanRoof : BaseSnowLoadRoof
    {
        public double LeftSlope { get; set; }
        public bool LeftSnowFences { get; set; }
        public double RightSlope { get; set; }
        public bool RightSnowFences { get; set; }
    }
}
