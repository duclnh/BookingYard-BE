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

	public async Task<IList<Facility>> GetFacilitiesTop(int numberTake, CancellationToken cancellationToken = default)
	{
		return await _dbContext.Facilities
					.Where(x => !x.IsDeleted)
					.Include(x => x.FeedBacks)
					.OrderByDescending(x => x.FeedBacks.Average(fb => fb.Rating))
					.Take(numberTake)
					.ToListAsync(cancellationToken);
	}
}
