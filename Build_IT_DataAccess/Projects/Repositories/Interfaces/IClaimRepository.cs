﻿using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.Projects.Entites;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.Projects.Repositories.Interfaces
{
    public interface IClaimRepository : IRepository<Claim>
    {
        Task<IEnumerable<Claim>> GetClaimsForUserAndProject(string userId, int projectId, CancellationToken cancellationToken);
        Task<Claim> FindAsync(string claimName, CancellationToken cancellationToken);
    }
}
