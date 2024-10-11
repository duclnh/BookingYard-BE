using System.Linq.Expressions;
using Fieldy.BookingYard.Domain.Abstractions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : class
    {
        protected readonly BookingYardDBContext _dbContext;
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

            if (includes.Length > 0)
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
            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IPagingList<TEntity>> FindAllPaging(
            int currentPage,
            int pageSize,
            Expression<Func<TEntity, bool>>[]? expressions,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            if (expressions != null && expressions.Any())
            {
                foreach (var expression in expressions)
                {
                    query = query.Where(expression);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await PagingList<TEntity>.CreateAsync(query, currentPage, pageSize, cancellationToken);
        }

        public async Task<IPagingList<TEntity>> FindAllPaging(
            int currentPage,
            int pageSize,
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

            return await PagingList<TEntity>.CreateAsync(query, currentPage, pageSize, cancellationToken);
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

        public async Task<ICollection<TEntity>> GetAll(Expression<Func<TEntity, bool>>? expression = null,CancellationToken cancellationToken = default)
        {
			IQueryable<TEntity> query = _dbSet;

			if (expression != null)
            {
				query = query.Where(expression);
			}

			return await query.AsNoTracking().ToListAsync(cancellationToken);
		}
    }
}