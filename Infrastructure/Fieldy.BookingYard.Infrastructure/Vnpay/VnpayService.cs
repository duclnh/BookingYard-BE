using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Application.Models.Vnpay;
using Fieldy.BookingYard.Infrastructure.Vnpay.Request;
using Fieldy.BookingYard.Infrastructure.Vnpay.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Transactions;

namespace Fieldy.BookingYard.Infrastructure.Vnpay
{
	public class VnpayService : IVnpayService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly VnpayConfig _vnpayConfig;
		public VnpayService(IHttpContextAccessor httpContextAccessor, IOptions<VnpayConfig> vnpayConfigOptions)
		{
			_httpContextAccessor = httpContextAccessor;
			_vnpayConfig = vnpayConfigOptions.Value;
		}
		public string CreateRequestUrl(decimal amount, string orderInfo, DateTime requestTime)
		{
			var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
			var vnpayPayRequest = new VnpayPayRequest(_vnpayConfig.Version,
								_vnpayConfig.TmnCode, requestTime, ipAddress, amount, _vnpayConfig.CurrCode ?? string.Empty,
								"other", orderInfo ?? string.Empty, _vnpayConfig.ReturnUrl, DateTime.Now.Ticks.ToString());
			return vnpayPayRequest.CreateRequestUrl(_vnpayConfig.PaymentUrl, _vnpayConfig.HashSecret);
		}

		public bool IsValidSignature(string vnp_Amount, string vnp_BankCode, string vnp_BankTranNo, string vnp_CardType,
										 string vnp_OrderInfo, string vnp_PayDate, string vnp_ResponseCode, string vnp_TmnCode,
										 string vnp_TransactionNo, string vnp_TransactionStatus, string vnp_TxnRef, string vnp_SecureHash)
		{
			var vnpayPayResponse = new VnpayPayResponse(vnp_Amount, vnp_BankCode, vnp_BankTranNo, vnp_CardType, vnp_OrderInfo, vnp_PayDate,
														vnp_ResponseCode, vnp_TmnCode, vnp_TransactionNo, vnp_TransactionStatus, vnp_TxnRef, 
														vnp_SecureHash);
			return vnpayPayResponse.IsValidSignature(_vnpayConfig.HashSecret);
		}
	}
}
