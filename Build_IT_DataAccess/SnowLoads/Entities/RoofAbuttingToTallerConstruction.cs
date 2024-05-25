namespace Build_IT_DataAccess.SnowLoads.Entities
{
    public class RoofAbuttingToTallerConstruction : BaseSnowLoadRoof
    {
        public double WidthOfUpperBuilding { get; set; }
        public bool WidthOfLowerBuilding { get; set; }
        public double HeightDifference { get; set; }
        public bool SlopeOfHigherRoof { get; set; }
        public bool SnowFencesOnHigherRoof { get; set; }
    }
}
