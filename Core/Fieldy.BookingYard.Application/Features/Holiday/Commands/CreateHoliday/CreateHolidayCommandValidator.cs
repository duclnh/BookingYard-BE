using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Holiday.Commands.CreateHoliday
{
	public class CreateHolidayCommandValidator : AbstractValidator<CreateHolidayCommand>
	{
		public CreateHolidayCommandValidator()
		{
		}
	}
}
