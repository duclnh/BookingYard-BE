namespace Fieldy.BookingYard.Application.Features.CollectVoucher.Queries
{
	public class CollectVoucherDto
	{
		public Guid CollectVoucherID { get; set; }
		public Guid UserID { get; set; }
		public Guid VoucherID { get; set; }
		public bool IsUsed { get; set; }
	}
}
