namespace Fieldy.BookingYard.Application.Features.Booking.Queries.DTO
{
	public class CustomerBooking
	{
		public Guid BookingID { get; set; }
		public string? PaymentCode { get; set; }
		public string? FacilityID { get; set; }
		public string? FacilityImage { get; set; }
		public string? FacilityLogo { get; set; }
		public string? FacilityName { get; set; }
		public string? StartTime { get; set; }
		public string? EndTime { get; set; }
		public string? PlayDate { get; set; }
		public string? BookingDate { get; set; }
		public decimal TotalPrice { get; set; }
		public bool PaymentStatus { get; set; }
		public bool BookingStatus { get; set; }
		public bool IsCheckIn { get; set; }
		public bool IsFeedback { get; set; }
		public bool IsDeleted { get; set; }
	}
}
