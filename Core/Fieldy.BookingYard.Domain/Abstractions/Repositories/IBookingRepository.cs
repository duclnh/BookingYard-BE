using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories
{
	public interface IBookingRepository : IRepositoryBase<Booking, Guid>
	{
		Task<Booking?> Find(Guid bookingID, CancellationToken cancellationToken);
	}
}
