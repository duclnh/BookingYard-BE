using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Application.Models.Vnpay;
using MediatR;
using Microsoft.Extensions.Options;

namespace Fieldy.BookingYard.Application.Features.Payment.Commands.CreatePayment
{
	public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, string>
	{
		private readonly IVnpayService _paymentService;
		private readonly VnpayConfig _vnpayConfig;

		public CreatePaymentCommandHandler(IVnpayService paymentService, IOptions<VnpayConfig> vnpayConfigOptions)
		{
			_paymentService = paymentService;
			_vnpayConfig = vnpayConfigOptions.Value;
		}

		public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
		{
			try
			{
				return _paymentService.CreateRequestUrl(_vnpayConfig.Version,
								_vnpayConfig.TmnCode, DateTime.Now, request.RequiredAmount, _vnpayConfig.CurrCode ?? string.Empty,
								"other", request.BookingId.ToString() ?? string.Empty, _vnpayConfig.ReturnUrl, DateTime.Now.Ticks.ToString(), _vnpayConfig.PaymentUrl, _vnpayConfig.HashSecret);
			}
			catch (Exception ex)
			{
				// Log the exception or handle it as needed
				Console.WriteLine($"An error occurred: {ex.Message}");
				// Optionally rethrow the exception or return a default value
				throw; // or return null;
			}
		}
	}
}
