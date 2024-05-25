namespace Build_IT_DataAccess.Projects.Entites
{
    public sealed class ProjectDeadLoad
    {
        public Project Project { get; init; }
        public int ProjectId { get; init; }
        public DeadLoad DeadLoad { get; init; }
        public int DeadLoadId { get; init; }
    }
}
