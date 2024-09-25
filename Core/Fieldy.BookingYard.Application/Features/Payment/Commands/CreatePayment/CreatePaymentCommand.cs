using Fieldy.BookingYard.Application.Features.Payment.Dtos;
using Fieldy.BookingYard.Application.Models.Payment;
using Fieldy.BookingYard.Application.Models.Query;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Payment.Commands.CreatePayment
{
	public record CreatePaymentCommand(TypePayment? typePayment) : IRequest<string>
	{
		public Guid BookingID { get; set; }
		public decimal Amount { get; set; }
		public DateTime CreatedDate { get; set; }
		public TypePayment TypePayment { get; set; }
	}
}
