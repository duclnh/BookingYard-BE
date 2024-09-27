namespace Fieldy.BookingYard.Application.Abstractions.Vnpay
{
	public interface IVnpayService
	{
		string CreateRequestUrl(decimal amount, string orderInfo);
	}
}
