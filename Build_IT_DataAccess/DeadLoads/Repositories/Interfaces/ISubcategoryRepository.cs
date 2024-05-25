using Build_IT_DataAccess.DeadLoads.Entities;
using Build_IT_DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.DeadLoads.Repositories.Interfaces
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<List<Subcategory>> GetAllSubcategoriesForCategoryAsync(long categoryId, CancellationToken cancellationToken);
    }
}
