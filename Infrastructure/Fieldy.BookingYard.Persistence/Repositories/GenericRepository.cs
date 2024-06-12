using System.Linq.Expressions;
using System.Reflection;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Query;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
    where T : class
    {
        private readonly BookingYardDBContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(BookingYardDBContext bookingYardDBContext)
        {
            _dbContext = bookingYardDBContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(filterExpression, cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(filterExpression, cancellationToken);
        }

        public async Task<T?> Get(
                Expression<Func<T, bool>> expression,
                List<string>? includes = null,
                CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<IList<T>> GetAll(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IPagingList<T>> GetAllPaging(
            RequestParams requestParams,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            
            return await PagingList<T>.CreateAsync(query, requestParams.CurrentPage, requestParams.PageSize, cancellationToken);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Entry(entity).State = EntityState.Modified;
        }
    }
}