using System;
using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Sport.Commands.CreateSport;

public class CreateSportCommandValidator : AbstractValidator<CreateSportCommand>
{
    public CreateSportCommandValidator()
    {
        
    }
}
