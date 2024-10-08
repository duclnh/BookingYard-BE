using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories
{
    public class SupportRepository : RepositoryBase<Support, int>, ISupportRepository
    {
        public SupportRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
        {
        }
    }
}
