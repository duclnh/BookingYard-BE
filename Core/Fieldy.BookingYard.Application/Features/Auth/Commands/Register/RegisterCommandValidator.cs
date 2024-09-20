using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        private readonly IUserRepository _userRepository;
        public RegisterCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(u => u.Name)
               .NotNull().WithMessage("Name not null")
               .NotEmpty().WithMessage("Name cannot be empty")
               .MaximumLength(30).WithMessage("Name not exceed 50 characters");
                

            RuleFor(u => u.Password)
                .NotNull().WithMessage("Password not null")
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(5).WithMessage("Password must be at least 5 characters")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");

            // RuleFor(u => u.Address)
            //     .NotNull().WithMessage("Address not null")
            //     .NotEmpty().WithMessage("Address cannot be empty")
            //     .MaximumLength(255).WithMessage("Name not exceed 255 characters");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email format")
                .MustAsync(EmailUnique).WithMessage("Email already exists")
                .MaximumLength(255).WithMessage("Email not exceed 255 characters");

            // RuleFor(u => u.Phone)
            //     .NotEmpty().WithMessage("Phone cannot be empty")
            //     .Matches(@"^0\d{9}(\d{2})?$").WithMessage("Phone start 0 and have 10-12 number")
            //     .MustAsync(Phone).WithMessage("Phone already exists");

            // RuleFor(u => u.Gender)
            //     .NotNull().WithMessage("Gender cannot be null");
        }

        private async Task<bool> EmailUnique(string email, CancellationToken cancellationToken = default)
        {
            return !await _userRepository.AnyAsync(x => x.Email.ToLower() == email.ToLower().Trim() && x.IsBanned == false, cancellationToken);
        }
    }
}