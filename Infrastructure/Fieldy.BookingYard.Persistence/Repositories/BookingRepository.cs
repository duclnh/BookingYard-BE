using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class BookingRepository : RepositoryBase<Booking, Guid>, IBookingRepository
	{
		public BookingRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{
		}
	}
}
