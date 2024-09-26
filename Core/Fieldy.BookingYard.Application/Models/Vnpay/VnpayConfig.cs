namespace Fieldy.BookingYard.Application.Models.Vnpay
{
	public class VnpayConfig
	{
		public static string ConfigName => "VNPAY";
		public string Version { get; set; } = string.Empty;
		public string TmnCode { get; set; } = string.Empty;
		public string HashSecret { get; set; } = string.Empty;
		public string ReturnUrl { get; set; } = string.Empty;
		public string PaymentUrl { get; set; } = string.Empty;
		public string CurrCode { get; set; } = string.Empty;
		public string Command { get; set; } = string.Empty;
		public string Locale { get; set; } = string.Empty;
	}
}
