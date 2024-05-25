using Build_IT_Data.Models.SnowLoads.Enums;
using Build_IT_DataAccess.Common;


namespace Build_IT_DataAccess.SnowLoads.Entities
{
    public abstract class BaseSnowLoadRoof : BaseAuditableEntity
    {
        public int SnowLoadId { get; set; }
        public SnowLoad  SnowLoad { get; set; }
        public double? AltitudeAboveSea { get; set; }
        public Zones CurrentZone { get; set; }
        public Topographies CurrentTopography { get; set; }

       public double? ThermalCoefficient { get; set; }
       public double? OverallHeatTransferCoefficient { get; set; }
       public double? InternalTemperature { get; set; }
       public double? TempreatureDifference { get; set; }
    }
}
