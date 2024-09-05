using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories
{
    public class SupportRepository : RepositoryBase<Support, Support>, ISupportRepository
    {
        public SupportRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
        {
        }
    }
}
