namespace Build_IT_DataAccess.DeadLoads.Entities 
{ 
    public sealed class MaterialAddition
    {
        public int AdditionId { get; init; }
        public Addition Addition { get; init; }
        public int MaterialId { get; init; }
        public Material Material { get; init; }
    }
}
