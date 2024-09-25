using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.Abstractions.Vnpay
{
	public interface IVnpayService
	{
		void AddRequestData(string key, string value);
		void AddResponseData(string key, string value);
		string GetResponseData(string key);
		string CreateRequestUrl(string baseUrl, string vnp_HashSecret);
		String HmacSHA512(string key, String inputData);
		string GetIpAddress();
	}
}
