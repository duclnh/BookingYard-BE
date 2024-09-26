using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Infrastructure.Vnpay.Request;
using Microsoft.AspNetCore.Http;
using System.Transactions;

namespace Fieldy.BookingYard.Infrastructure.Vnpay
{
	public class VnpayService : IVnpayService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public VnpayService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		public string CreateRequestUrl(string version, string tmnCode, DateTime createDate,
			decimal amount, string currCode, string orderType, string orderInfo,
			string returnUrl, string txnRef, string baseUrl, string secretKey)
		{
			var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
			var vnpayPayRequest = new VnpayPayRequest(version, tmnCode, createDate, ipAddress, amount, currCode, orderType, orderInfo, returnUrl, txnRef);
			return vnpayPayRequest.CreateRequestUrl(baseUrl, secretKey);
		}
	}
}
