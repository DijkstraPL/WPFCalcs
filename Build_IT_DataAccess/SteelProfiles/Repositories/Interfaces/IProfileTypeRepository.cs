using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.SteelProfiles.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.SteelProfiles.Repositories.Interfaces
{
    public interface IProfileTypeRepository : IRepository<ProfileType>
    {
        Task<IEnumerable<ProfileType>> GetAllProfileTypesAsync();
    }
}
