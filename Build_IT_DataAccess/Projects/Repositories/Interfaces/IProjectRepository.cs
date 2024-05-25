using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.Projects.Entites;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.Projects.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetAllForCompanyAsync(int companyId, CancellationToken cancellationToken = default);
    }
}
