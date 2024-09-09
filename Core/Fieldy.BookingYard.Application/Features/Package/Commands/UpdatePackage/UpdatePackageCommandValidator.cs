using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Package.Commands.UpdatePackage
{
    public class UpdatePackageCommandValidator : AbstractValidator<UpdatePackageCommand>
    {
        public UpdatePackageCommandValidator()
        {
            RuleFor(command => command.PackageName)
            .NotEmpty().WithMessage("Package name is required.")
            .Length(1, 100).WithMessage("Package name must be between 1 and 100 characters.");
        }
    }
}
