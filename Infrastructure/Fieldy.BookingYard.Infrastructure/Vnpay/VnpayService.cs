using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Application.Models.Vnpay;
using Fieldy.BookingYard.Infrastructure.Vnpay.Request;
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
		public string CreateRequestUrl(decimal amount, string orderInfo)
		{
			var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
			var vnpayPayRequest = new VnpayPayRequest(_vnpayConfig.Version,
								_vnpayConfig.TmnCode, DateTime.Now, ipAddress, amount, _vnpayConfig.CurrCode ?? string.Empty,
								"other", orderInfo ?? string.Empty, _vnpayConfig.ReturnUrl, DateTime.Now.Ticks.ToString());
			return vnpayPayRequest.CreateRequestUrl(_vnpayConfig.PaymentUrl, _vnpayConfig.HashSecret);
		}
	}
}
