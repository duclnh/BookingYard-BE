using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class BookingRepository : RepositoryBase<Booking, Guid>, IBookingRepository
	{
		public BookingRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{
		}
		public async Task<Booking?> Find(Guid bookingID, CancellationToken cancellationToken)
		{
			var booking = await _dbContext.Bookings
										.Where(x => x.Id == bookingID)
										.Include(x => x.Court)
										.ThenInclude(x => x.Facility)
										.FirstOrDefaultAsync(cancellationToken);
			if(booking == null)
			{
				return null;
			}
			return booking;
		}
	}
}
