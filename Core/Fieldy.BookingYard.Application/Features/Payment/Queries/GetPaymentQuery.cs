using Fieldy.BookingYard.Application.Features.Payment.Dtos;
using Fieldy.BookingYard.Application.Models.Payment;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Payment.Queries
{
	public class GetPaymentQuery : IRequest<BaseResultWithData<PaymentDtos>>
	{
		public string Id { get; set; } = string.Empty;
	}
}
