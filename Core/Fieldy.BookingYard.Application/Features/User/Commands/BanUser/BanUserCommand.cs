using System;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.User.Commands.BanUser;

public class BanUserCommand : IRequest<string>
{
    public Guid UserID { get; set; }
}
