using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Booking.Commands.CreateBooking
{
	public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
	{
		public CreateBookingCommandValidator()
		{
			RuleFor(x => x.CourtID).NotEmpty().WithMessage("CourtID is required.");
			RuleFor(x => x.UserID).NotEmpty().WithMessage("CustomerID is required.");
		}
	}
}
