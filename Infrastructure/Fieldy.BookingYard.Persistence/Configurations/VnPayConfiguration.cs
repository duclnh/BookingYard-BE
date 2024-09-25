using Microsoft.Extensions.Configuration;

namespace Fieldy.BookingYard.Persistence.Configurations
{
	public class VnPayConfiguration
	{
		public static string ConfigName => "VNPAY";
		public string Version { get; set; } = string.Empty;
		public string TmnCode { get; set; } = string.Empty;
		public string HashSecret { get; set; } = string.Empty;
		public string ReturnUrl { get; set; } = string.Empty;
		public string PaymentUrl { get; set; } = string.Empty;
	}
}
