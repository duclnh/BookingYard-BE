using System;
using FluentValidation;

namespace Fieldy.BookingYard.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {

    }
}
