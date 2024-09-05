using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.Google
{
    public class GoogleCommandValidator : AbstractValidator<GoogleCommand>
    {
        public GoogleCommandValidator()
        {
            RuleFor(x => x.GoogleID)
                .NotNull().WithMessage("GoogleID not null")
                .NotEmpty().WithMessage("GoogleID cannot be empty");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name not null")
                .NotEmpty().WithMessage("Name cannot be empty");

            RuleFor(x => x.ImageUrl)
                .NotNull().WithMessage("ImageUrl not null")
                .NotEmpty().WithMessage("ImageUrl cannot be empty");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email not exceed 255 characters");
        }
    }
}