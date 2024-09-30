using MediatR;

namespace Fieldy.BookingYard.Application.Features.Payment.Commands.CreatePayment
{
	public record CreatePaymentCommand: IRequest<string>
	{
		public Guid BookingId { get; set; }
		public int RequiredAmount { get; set; }
	}
}
