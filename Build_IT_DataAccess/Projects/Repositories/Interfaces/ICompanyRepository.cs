using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.Projects.Entites;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.Projects.Repositories.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<List<Company>> GetAllForUserAsync(string userId, CancellationToken cancellationToken = default);
    }
}
