using Build_IT_DataAccess.Common;

namespace Build_IT_DataAccess.Projects.Entites
{
    public sealed class UserCompany 
    {
        public int CompanyId { get; init; }
        public Company Company { get; init; }
        public string UserId { get; init; }
    }
}
