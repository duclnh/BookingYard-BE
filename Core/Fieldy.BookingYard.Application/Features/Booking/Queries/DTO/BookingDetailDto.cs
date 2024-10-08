using Fieldy.BookingYard.Domain.Enums;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.DTO
{
	public class BookingDetailDto
	{
		public Guid BookingID { get; set; }
		public Guid FacilityID { get; set; }
		public string? PaymentCode { get; set; }
		public string? FacilityLogo { get; set; }
		public string? FacilityName { get; set; }
		public string? FacilityImage { get; set; }
		public string? FullAddress { get; set; }
		public int CourtID { get; set; }
		public string? CourtName { get; set; }
		public string? CourtImage { get; set; }
		public string? Court360 { get; set; }
		public string? CourtType { get; set; }
		public string? BookingName { get; set; }
		public string? BookingPhone { get; set; }
		public string? PaymentMethod { get; set; }
		public bool PaymentStatus { get; set; }
		public string? StartTime { get; set; }
		public string? EndTime { get; set; }
		public string? PlayDate { get; set; }
		public string? BookingDate { get; set; }
		public Guid? VoucherID { get; set; }
		public string? VoucherName { get; set; }
		public int Percentage { get; set; }
		public string? VoucherCode { get; set; }
		public string? VoucherFacility { get; set; }
		public string? VoucherStartDate { get; set; }
		public string? VoucherEndDate { get; set; }
		public string? VoucherSport { get; set; }
		public string? Reason { get; set; }
		public string? SportName { get; set; }
		public decimal CourtPrice { get; set; }
		public decimal TotalPrice { get; set; }
		public bool IsDeleted { get; set; }
	}
}
