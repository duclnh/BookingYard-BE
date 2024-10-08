using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Payment.Commands.CreatePayment
{
	public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
	{
		public CreatePaymentCommandValidator()
		{
		}
	}
}
