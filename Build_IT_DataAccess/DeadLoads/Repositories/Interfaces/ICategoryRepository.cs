using Build_IT_DataAccess.DeadLoads.Entities;
using Build_IT_DataAccess.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.DeadLoads.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IQueryable<Category> GetAllCategoriesAsync();
    }
}
