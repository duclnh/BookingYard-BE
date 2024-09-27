namespace Fieldy.BookingYard.Domain.Entities
{
	public class VnpayPayment
	{
		public Guid BookingID { get; set; }
		public decimal Amount { get; set; }
		public string BankCode { get; set; } = string.Empty;
		public string ReturnUrl { get; set; } = string.Empty;
	}
}
