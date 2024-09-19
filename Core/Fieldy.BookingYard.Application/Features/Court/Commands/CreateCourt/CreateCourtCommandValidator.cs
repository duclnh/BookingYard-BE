using System;
using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.Court.Commands.CreateCourt;

public class CreateCourtCommandValidator : AbstractValidator<CreateCourtCommand>
{
    public CreateCourtCommandValidator()
    {
        
    }
}
