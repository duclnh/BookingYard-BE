using System.Linq.Expressions;
using Fieldy.BookingYard.Application.Models.Query;

namespace Fieldy.BookingYard.Application.Contracts.Persistence
{
    public interface IRepositoryBase<TEntity, in TKey>
        where TEntity : class
    {
        Task<TEntity?> FindByIdAsync(
            TKey id, 
            CancellationToken cancellationToken= default,
            params Expression<Func<TEntity,object>>[] includes
        );
        Task<IList<TEntity>> FindAll(
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            List<string>? includes = null,
            CancellationToken cancellationToken = default
        );
        Task<IPagingList<TEntity>> FindAllPaging(
            RequestParams requestParams,
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            List<string>? includes = null,
            CancellationToken cancellationToken = default
        );
        Task<TEntity?> Find(
            Expression<Func<TEntity, bool>> expression,
            List<string>? includes = null,
            CancellationToken cancellationToken = default
        );
        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> filterExpression,
            CancellationToken cancellationToken = default
        );
        Task<int> CountAsync(
            Expression<Func<TEntity, bool>> filterExpression,
            CancellationToken cancellationToken = default
        );
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    }
}