using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetUnpaidBooking
{
	public record GetUnpaidBooking(Guid userID, CancellationToken cancellationToken) : IRequest<BookingDetailDto>
	{
	}
}
