using Build_IT_DataAccess.Projects.Entites;

namespace Build_IT_DataAccess.SnowLoads.Entities
{
    public class ProjectSnowLoad
    {
        public Project Project { get; init; }
        public int ProjectId { get; init; }
        public SnowLoad SnowLoad { get; init; }
        public int SnowLoadId { get; init; }
    }
}
