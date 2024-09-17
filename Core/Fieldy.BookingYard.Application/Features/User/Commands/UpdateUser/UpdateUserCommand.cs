using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Fieldy.BookingYard.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<string>
{
    public required Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public int? WardID { get; set; }
    public IFormFile? Image { get; set; }
}
