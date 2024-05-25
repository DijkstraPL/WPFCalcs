using Build_IT_DataAccess.Common;


namespace Build_IT_DataAccess.SnowLoads.Entities
{
    public class SnowLoad : BaseAuditableEntity
    {
        public string Name { get; set; }
        public int SnowLoadRoofId { get; set; }
        public BaseSnowLoadRoof SnowLoadRoof { get; set; }
    }
}
