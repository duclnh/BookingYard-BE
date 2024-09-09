using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Package.Commands.CreatePackage
{
    public class CreatePackageCommandValidator : AbstractValidator<CreatePackageCommand>
    {
        public CreatePackageCommandValidator()
        {
            RuleFor(command => command.PackageName)
            .NotEmpty().WithMessage("Package name is required.")
            .Length(1, 100).WithMessage("Package name must be between 1 and 100 characters.");
            RuleFor(command => command.PackagePrice)
            .NotEmpty().WithMessage("Package price is required.")
            .GreaterThan(0).WithMessage("Package price must be greater than 0.");
        }
    }
}
