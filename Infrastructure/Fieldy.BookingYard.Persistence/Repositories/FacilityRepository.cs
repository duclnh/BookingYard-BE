using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories;

public class FacilityRepository : RepositoryBase<Facility, Guid>, IFacilityRepository
{
    public FacilityRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
    {
    }
}
