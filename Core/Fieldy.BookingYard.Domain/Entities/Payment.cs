using Fieldy.BookingYard.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
	[Table("Payment")]
	public class Payment : EntityBase<string>
	{
		public string PaymentContent { get; set; } = string.Empty;
		public string PaymentCurrency { get; set; } = string.Empty;
		public string PaymentRefId { get; set; } = string.Empty;
		public decimal? RequiredAmount { get; set; }
		public DateTime? PaymentDate { get; set; } = DateTime.Now;
		public DateTime? ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);
		public string? PaymentLanguage { get; set; } = string.Empty;
		public string? MerchantId { get; set; } = string.Empty;
		public string? PaymentDestinationId { get; set; } = string.Empty;
		public decimal? PaidAmount { get; set; }
		public string? PaymentStatus { get; set; } = string.Empty;
		public string? PaymentLastMessage { get; set; } = string.Empty;
	}
}
