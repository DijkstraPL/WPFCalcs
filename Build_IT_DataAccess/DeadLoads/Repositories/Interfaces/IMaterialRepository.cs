using Build_IT_DataAccess.DeadLoads.Entities;
using Build_IT_DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.DeadLoads.Repositories.Interfaces
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<List<Material>> GetAllMaterialsForSubcategoryAsync(long subcategoryId, CancellationToken cancellationToken);
    }
}
