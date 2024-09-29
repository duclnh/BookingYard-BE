using Fieldy.BookingYard.Domain.Abstractions;

namespace Fieldy.BookingYard.Domain.Entities
{
	public class Transaction : EntityBase<Guid>
	{
		public int vnp_Amount { get; set; } = 0;
		public string vnp_BankCode { get; set; } = string.Empty;
		public string vnp_BankTranNo { get; set; } = string.Empty;
		public string vnp_CardType { get; set; } = string.Empty;
		public string vnp_OrderInfo { get; set; } = string.Empty;
		public string vnp_PayDate { get; set; } = string.Empty;
		public string vnp_ResponseCode { get; set; } = string.Empty;
		public string vnp_TmnCode { get; set; } = string.Empty;
		public string vnp_TransactionNo { get; set; } = string.Empty;
		public string vnp_TransactionStatus { get; set; } = string.Empty;
		public string vnp_TxnRef { get; set; } = string.Empty;
		public string vnp_SecureHash { get; set; } = string.Empty;
	}
}
