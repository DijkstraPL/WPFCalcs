using System;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CompleteAsync(CancellationToken cancellationToken);
        Task<int> CompleteAsync();
    }
}
