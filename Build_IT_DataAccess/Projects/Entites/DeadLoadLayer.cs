using Build_IT_DataAccess.DeadLoads.Entities;

namespace Build_IT_DataAccess.Projects.Entites
{
    public sealed class DeadLoadLayer
    {
        public int Id { get; init; }
        public int MaterialId { get; init; }
        public Material Material { get; init; }
        public int DeadLoadId { get; init; }
        public DeadLoad DeadLoad { get; init; }
        public double? Width { get; init; }
        public double? Height { get; init; }
        public double? Length { get; init; }

        public int? PreviousDeadLoadId { get; init; }
        public DeadLoadLayer? PreviousDeadLoad { get; init; }
    }
}
