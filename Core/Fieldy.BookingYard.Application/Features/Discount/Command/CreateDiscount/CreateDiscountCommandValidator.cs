﻿using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Discount.Command.CreateDiscount
{
	public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
	{
		public CreateDiscountCommandValidator()
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