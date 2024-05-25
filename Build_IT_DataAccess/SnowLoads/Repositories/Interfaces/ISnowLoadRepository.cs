using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.SnowLoads.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.SnowLoads.Repositories.Interfaces
{
    public interface ISnowLoadRepository : IRepository<SnowLoad>
    {
        Task< IEnumerable<SnowLoad>> GetAllSnowLoadsAsync(int projectId, CancellationToken cancellationToken);
        Task AddProjectMapping(ProjectSnowLoad projectEntityMapping, CancellationToken cancellationToken);
        Task AddRoof(MonopitchRoof roof);
        Task AddRoof(PitchedRoof roof);
        Task AddRoof(MultiSpanRoof multiSpanRoof);
        Task AddRoof(CylindricalRoof cylindricalRoof);
        Task AddRoof(DriftingAtProjectionsObstructions driftingAtProjectionsObstructions);
        Task AddRoof(RoofAbuttingToTallerConstruction roofAbuttingToTallerConstruction);
        Task AddRoof(Snowguards snowguards);
        Task AddRoof(SnowOverhanging snowOverhanging);
    }
}
