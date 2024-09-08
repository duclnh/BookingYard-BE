using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Discount.Command.UpdateDiscount
{
	public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
	{
		public UpdateDiscountCommandValidator()
		{
			RuleFor(command => command.DiscountName)
			.NotEmpty().WithMessage("Discount name is required.")
			.Length(1, 100).WithMessage("Discount name must be between 1 and 100 characters.");
			RuleFor(command => command.Percentage)
			.GreaterThan(-1).WithMessage("Percentage must be between 1 and 100.")
			.LessThan(101).WithMessage("Percentage must be between 1 and 100.");
		}
	}
}
