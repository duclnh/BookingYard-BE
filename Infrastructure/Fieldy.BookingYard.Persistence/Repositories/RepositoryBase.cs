using System.Linq.Expressions;
using System.Reflection;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Query;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : class
    {
        private readonly BookingYardDBContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public RepositoryBase(BookingYardDBContext bookingYardDBContext)
        {
            _dbContext = bookingYardDBContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(filterExpression, cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(filterExpression, cancellationToken);
        }

        public async Task<TEntity?> Find(
                Expression<Func<TEntity, bool>> expression,
                CancellationToken cancellationToken = default,
                params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<IList<TEntity>> FindAll(
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
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

        public async Task<IPagingList<TEntity>> FindAllPaging(
            RequestParams requestParams,
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

			if (includes != null)
			{
				foreach (var includeProperty in includes)
				{
					query = query.Include(includeProperty);
				}
			}

			if (expression != null)
            {
                query = query.Where(expression);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // if (!string.IsNullOrEmpty(requestParams.Search) && requestParams.SearchBy != null)
            // {
            //     var param = Expression.Parameter(typeof(TEntity), "x");
            //     var body = (Expression)param;

            //     foreach (var search in requestParams.SearchBy)
            //     {
            //         body = Expression.PropertyOrField(body, search);
            //     }
            //     body = Expression.Call(body, "ToLower", Type.EmptyTypes);
            //     body = Expression.Call(typeof(DbFunctionsExtensions), "Like", Type.EmptyTypes,
            //         Expression.Constant(EF.Functions), body, Expression.Constant($"%{requestParams.Search}%".ToLower()));

            //     var lambda = Expression.Lambda(body, param);

            //     var queryExpr = Expression.Call(typeof(Queryable), "Where", new[] { typeof(TEntity) }, query.Expression, lambda);

            //     query.Provider.CreateQuery<TEntity>(queryExpr);
            // }

            return await PagingList<TEntity>.CreateAsync(query, requestParams.CurrentPage, requestParams.PageSize, cancellationToken);
        }

        public async Task<TEntity?> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            query = query.Where(e => EF.Property<TKey>(e, "Id")!.Equals(id));


            return await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Entry(entity).State = EntityState.Modified;
        }
    }
}