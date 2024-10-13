using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Commands.CancelBooking;

public class CancelBookingCommand : IRequest<string>
{
	public Guid BookingID { get; set; }
	public required string Reason { get; set; }
	public string? PaymentCode { get; set; }
}
