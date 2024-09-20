using System.Linq.Expressions;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories
{
    public interface IRepositoryBase<TEntity, in TKey>
        where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }
        Task<TEntity?> FindByIdAsync(
            TKey id,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes
        );
        Task<IList<TEntity>> FindAll(
           Expression<Func<TEntity, bool>>? expression = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           CancellationToken cancellationToken = default,
           params Expression<Func<TEntity, object>>[] includes
       );
        Task<IPagingList<TEntity>> FindAllPaging(
            int currentPage,
            int pageSize,
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes
        );
        Task<IPagingList<TEntity>> FindAllPaging(
           int currentPage,
           int pageSize,
           Expression<Func<TEntity, bool>>[]? expressions = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           CancellationToken cancellationToken = default,
           params Expression<Func<TEntity, object>>[] includes
       );
        Task<TEntity?> Find(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes
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