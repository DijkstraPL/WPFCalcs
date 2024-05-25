using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.Projects.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.Projects.Repositories.Interfaces
{
    public interface IDeadLoadRepository : IRepository<DeadLoad>
    {
       Task< IEnumerable<DeadLoad>> GetAllForProjectAsync(int projectId, CancellationToken cancellationToken = default);
        Task AddProjectMapping(ProjectDeadLoad projectDeadLoad, CancellationToken cancellationToken);
        Task<IEnumerable<DeadLoadLayer>> GetLayers(int deadLoadId, CancellationToken cancellationToken = default);
        void RemoveLayers(int deadLoadId);
        Task AddLayersAsync(IEnumerable<DeadLoadLayer> deadLoadLayers, CancellationToken cancellationToken);
        Task AddLayerAsync(DeadLoadLayer deadLoadLayer, CancellationToken cancellationToken);
    }
}
