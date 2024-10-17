using System.Linq.Expressions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories;

public class FacilityRepository : RepositoryBase<Facility, Guid>, IFacilityRepository
{
	public FacilityRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
	{

	}

	public async Task<IList<Facility>> FindAllFacility(Expression<Func<Facility, bool>>[]? expressions = null, Func<IQueryable<Facility>, IOrderedQueryable<Facility>>? orderBy = null, CancellationToken cancellationToken = default, params Expression<Func<Facility, object>>[] includes)
	{
		IQueryable<Facility> query = _dbContext.Facilities;

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

		return await query.ToListAsync();
	}

	public async Task<IList<Facility>> GetFacilitiesTop(int numberTake, CancellationToken cancellationToken = default)
	{
		return await _dbContext.Facilities
					.AsNoTracking()
					.Where(x => !x.IsDeleted)
					.Include(x => x.FeedBacks)
					.OrderByDescending(x => x.FeedBacks.Average(fb => fb.Rating))
					.Take(numberTake)
					.ToListAsync(cancellationToken);
	}

    public async Task<IList<int>> GetFacilityProvince(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Facilities.AsNoTracking()
											.Where(x => x.IsDeleted == false)
											.Select(x => x.ProvinceID)
											.Distinct()
											.ToListAsync();
    }
}
