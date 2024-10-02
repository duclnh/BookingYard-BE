using Fieldy.BookingYard.Domain.Enums;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.DTO
{
	public class BookingDetailDto
	{
		public Guid BookingID { get; set; }
		public Guid FacilityID { get; set; }
		public string? PaymentCode { get; set; }
		public string? FacilityName { get; set; }
		public string? FullAddress { get; set; }
		public int CourtID { get; set; }
		public string? CourtName { get; set; }
		public string? CourtImage { get; set; }
		public string? Court360 { get; set; }
		public string? CourtType { get; set; }
		public string? BookingName { get; set; }
		public string? BookingPhone { get; set; }
		public TypePayment PaymentMethod { get; set; }
		public bool PaymentStatus { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public DateTime PlayDate { get; set; }
		public DateTime BookingDate { get; set; }
		public Guid? VoucherID { get; set; }
		public string? VoucherName { get; set; }
		public int Percentage { get; set; }
		public string? VoucherCode { get; set; }
		public decimal CourtPrice { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
