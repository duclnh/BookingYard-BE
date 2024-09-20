using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Voucher.Command.UpdateVoucher
{
	public class UpdateVoucherCommandValidator : AbstractValidator<UpdateVoucherCommand>
	{
		public UpdateVoucherCommandValidator()
		{
			RuleFor(command => command.VoucherName)
			.NotEmpty().WithMessage("Voucher name is required.")
			.Length(1, 100).WithMessage("Voucher name must be between 1 and 100 characters.");
			RuleFor(command => command.Percentage)
			.GreaterThan(-1).WithMessage("Percentage must be between 1 and 100.")
			.LessThan(101).WithMessage("Percentage must be between 1 and 100.");
		}
	}
}
