using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Application.Models.Vnpay;
using MediatR;
using Microsoft.Extensions.Options;

namespace Fieldy.BookingYard.Application.Features.Payment.Commands.CreatePayment
{
	public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, string>
	{
		private readonly IVnpayService _paymentService;

		public CreatePaymentCommandHandler(IVnpayService paymentService)
		{
			_paymentService = paymentService;
		}

		public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
		{
			try
			{
				return _paymentService.CreateRequestUrl(request.RequiredAmount, request.BookingId.ToString() ?? string.Empty);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred in vnpay payment: {ex.Message}");
				throw;
			}
		}
	}
}
