using System.Linq.Expressions;
using Fieldy.BookingYard.Application.Models.Query;

namespace Fieldy.BookingYard.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null,
            CancellationToken cancellationToken = default
        );
        Task<IPagingList<T>> GetAllPaging(
            RequestParams requestParams,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null,
            CancellationToken cancellationToken = default
        );
        Task<T?> Get(
            Expression<Func<T, bool>> expression,
            List<string>? includes = null,
            CancellationToken cancellationToken = default
        );
        Task<bool> AnyAsync(
            Expression<Func<T, bool>> filterExpression,
            CancellationToken cancellationToken = default
        );
        Task<int> CountAsync(
            Expression<Func<T, bool>> filterExpression,
            CancellationToken cancellationToken = default
        );
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}