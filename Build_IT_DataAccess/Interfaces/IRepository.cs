using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity> GetAsync(int id, CancellationToken cancellationToken = default);
        IQueryable<TEntity> GetAll();
       IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }

}
