namespace Fieldy.BookingYard.Application.Features.Booking.Queries.DTO
{
	public class CustomerBooking
	{
		public Guid BookingID { get; set; }
		public string? PaymentCode { get; set; }
		public string? FacilityImage { get; set; }
		public string? FacilityName { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public DateTime PlayDate { get; set; }
		public DateTime BookingDate { get; set; }
		public decimal TotalPrice { get; set; }
		public bool PaymentStatus { get; set; }
		public bool BookingStatus { get; set; }
		public bool IsCheckIn { get; set; }
		public bool IsFeedback { get; set; }
		public bool IsDelete { get; set; }
	}
}
