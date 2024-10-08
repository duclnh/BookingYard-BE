using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Commands.CreateBooking
{
	public class CreateBookingCommand : IRequest<string>
	{
		public required string FullName { get; set; }
		public required string Phone { get; set; }
		public string? Email { get; set; }
		public int CourtID { get; set; }
		public decimal CourtPrice { get; set; }
		public decimal TotalPrice { get; set; }
		public Guid UserID { get; set; }
		public required string BookingDate { get; set; }
		public required string StartTime { get; set; }
		public required string EndTime { get; set; }
		public Guid? CollectVoucherID { get; set; }
		public int Point { get; set; }
		public required string PaymentMethod { get; set; }
	}
}
