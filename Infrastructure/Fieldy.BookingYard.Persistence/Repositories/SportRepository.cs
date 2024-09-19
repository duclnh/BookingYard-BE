using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories;

public class SportRepository : RepositoryBase<Sport, int>, ISportRepository
{
    public SportRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
    {
    }
}