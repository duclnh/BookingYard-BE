using Fieldy.BookingYard.Domain.Enums;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.DTO
{
	public class BookingDetailDto
	{
		public string FullName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public Guid CourtID { get; set; }
		public decimal CourtPrice { get; set; }
		public decimal TotalPrice { get; set; }
		public Guid CustomerID { get; set; }
		public DateTime BookingDate { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public Guid? VoucherID { get; set; }
		public bool Status { get; set; }
		public bool PaymentStatus { get; set; }
		public TypePayment PaymentMethod { get; set; }
	}
}
