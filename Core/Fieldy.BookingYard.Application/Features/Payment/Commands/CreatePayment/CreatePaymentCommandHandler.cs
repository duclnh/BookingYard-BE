using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Application.Models.Vnpay;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Payment.Commands.CreatePayment
{
	public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, string>
	{
		private readonly IVnpayService _vnpay;
		private readonly VnpayConfig _vnpayConfig;

		public CreatePaymentCommandHandler(IVnpayService vnpay)
		{
			_vnpay = vnpay;
		}

		public Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
		{
			_vnpay.AddRequestData("vnp_Version", _vnpayConfig.Version);
			_vnpay.AddRequestData("vnp_Command", "pay");
			_vnpay.AddRequestData("vnp_TmnCode", _vnpayConfig.TmnCode);
			_vnpay.AddRequestData("vnp_Amount", request.Amount.ToString());
			if (request.TypePayment == Domain.Enums.TypePayment.VnpayQr)
			{
				_vnpay.AddRequestData("vnp_BankCode", "QRPAY");
			}
			else if (request.TypePayment == Domain.Enums.TypePayment.Vnbank)
			{
				_vnpay.AddRequestData("vnp_BankCode", "VNBANK");
			}
			else
			{
				_vnpay.AddRequestData("vnp_BankCode", "INTCARD");
			}
			_vnpay.AddRequestData("vnp_CreateDate", request.CreatedDate.ToString("yyyyMMddHHmmss"));
			_vnpay.AddRequestData("vnp_CurrCode", "VND");
			_vnpay.AddRequestData("vnp_IpAddr", _vnpay.GetIpAddress());
			_vnpay.AddRequestData("vnp_Locale", "vn");
			_vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + request.BookingID);
			_vnpay.AddRequestData("vnp_OrderType", "other");
			_vnpay.AddRequestData("vnp_ReturnUrl", _vnpayConfig.ReturnUrl);
			_vnpay.AddRequestData("vnp_TxnRef", request.BookingID.ToString());
			string paymentUrl = _vnpay.CreateRequestUrl(_vnpayConfig.PaymentUrl, _vnpayConfig.HashSecret);

			return Task.FromResult(paymentUrl);
		}
	}
}
