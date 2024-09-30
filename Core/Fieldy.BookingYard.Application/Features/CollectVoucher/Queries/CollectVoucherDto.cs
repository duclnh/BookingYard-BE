namespace Fieldy.BookingYard.Application.Features.CollectVoucher.Queries
{
	public class CollectVoucherDto
	{
		public Guid CollectVoucherID { get; set; }
		public Guid FacilityID { get; set; }
		public int Percentage { get; set; }
		public string? FacilityName { get; set; }
		public string? SportName { get; set; }
		public string? VoucherName { get; set; }
		public required string StartDate { get; set; }
		public required string EndDate { get; set; }
		public bool IsOutDate { get; set; }
		public bool IsUsed { get; set; }
	}
}
