using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.UpdatePassword
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {

            RuleFor(x => x.NewPassword)
               .NotNull().WithMessage("New password not null")
               .NotEmpty().WithMessage("New password cannot be empty")
               .MinimumLength(5).WithMessage("Password must be at least 5 characters");
        }
    }
}