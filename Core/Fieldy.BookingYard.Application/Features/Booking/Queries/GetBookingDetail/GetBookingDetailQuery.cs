using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetBookingDetail
{
	public record GetBookingDetailQuery(Guid bookingID, CancellationToken cancellationToken) : IRequest<BookingDetailDto>
	{
	}
}
