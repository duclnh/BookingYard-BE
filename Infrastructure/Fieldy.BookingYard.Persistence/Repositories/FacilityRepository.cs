using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories;

public class FacilityRepository : RepositoryBase<Facility, Guid>, IFacilityRepository
{
	private readonly BookingYardDBContext _bookingYardDBContext;
    public FacilityRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
    {
		_bookingYardDBContext = bookingYardDBContext;
    }

	public async Task<Facility> GetFacilityByID(Guid facilityID)
	{
		var result = await _bookingYardDBContext.Facilities
										.FindAsync(facilityID);
		return result;
	}
}
