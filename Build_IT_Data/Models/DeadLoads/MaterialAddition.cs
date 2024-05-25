namespace Build_IT_Data.Models.DeadLoads
{ 
    public class MaterialAddition
    {
        public long AdditionId { get; set; }
        public Addition Addition { get; set; }
        public long MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
