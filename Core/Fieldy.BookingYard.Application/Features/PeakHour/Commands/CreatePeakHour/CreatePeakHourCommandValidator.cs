using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.PeakHour.Commands.CreatePeakHour
{
	public class CreatePeakHourCommandValidator : AbstractValidator<CreatePeakHourCommand>
	{
		public CreatePeakHourCommandValidator()
		{
		}
	}
}
