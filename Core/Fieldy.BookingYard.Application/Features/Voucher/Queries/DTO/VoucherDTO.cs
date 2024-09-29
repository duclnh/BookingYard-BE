namespace Fieldy.BookingYard.Application.Features.Voucher.Queries
{
	public class VoucherDTO
	{
		public Guid VoucherID { get; set; }
		public string? VoucherName { get; set; }
		public string? Image { get; set; }
		public int Percentage { get; set; }
		public string? VoucherDescription { get; set; }
		public int quantity { get; set; }
		public required string RegisterDate { get; set; }
		public required string ExpiredDate { get; set; }
		public string? Reason { get; set; }
		public bool Status { get; set; }
	}
}
