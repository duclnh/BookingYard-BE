using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Facility.Commands.CreateFacility;

public class CreateFacilityCommandValidator : AbstractValidator<CreateFacilityCommand>
{
    public CreateFacilityCommandValidator()
    {
    }
}
