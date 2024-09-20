using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories;

public class CourtRepository : RepositoryBase<Court, int>, ICourtRepository
{
    public CourtRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
    {
    }
}
