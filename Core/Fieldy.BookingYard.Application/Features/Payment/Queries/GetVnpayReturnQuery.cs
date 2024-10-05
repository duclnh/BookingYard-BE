using MediatR;

namespace Fieldy.BookingYard.Application.Features.Payment.Queries
{
	public record GetVnpayReturnQuery : IRequest<Guid>
	{
		public required string vnp_Amount { get; set; }
		public required string vnp_BankCode { get; set; }
		public required string vnp_BankTranNo { get; set; }
		public required string vnp_CardType { get; set; }
		public required string vnp_OrderInfo { get; set; }
		public required string vnp_PayDate { get; set; }
		public required string vnp_ResponseCode { get; set; }
		public required string vnp_TmnCode { get; set; }
		public required string vnp_TransactionNo { get; set; }
		public required string vnp_TransactionStatus { get; set; }
		public required string vnp_TxnRef { get; set; }
		public required string vnp_SecureHash { get; set; }
	}
}
