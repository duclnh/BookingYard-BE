namespace Fieldy.BookingYard.Application.Abstractions.Vnpay
{
	public interface IVnpayService
	{
		string CreateRequestUrl(string version, string tmnCode, DateTime createDate,
			decimal amount, string currCode, string orderType, string orderInfo,
			string returnUrl, string txnRef, string baseUrl, string secretKey);
	}
}
