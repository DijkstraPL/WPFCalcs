namespace Build_IT_DataAccess.Projects.Entites
{
    public sealed class ProjectDesignerClaim
    {
        public int ClaimId { get; init; }
        public Claim Claim { get; init; }
        public int ProjectId { get; init; }
        public Project Project { get; init; }
        public string DesignerId { get; init; }
        public Designer Designer { get; init; }
    }
}
