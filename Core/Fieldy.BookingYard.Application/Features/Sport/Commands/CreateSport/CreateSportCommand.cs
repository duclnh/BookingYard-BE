using MediatR;
using Microsoft.AspNetCore.Http;

namespace Fieldy.BookingYard.Application.Features.Sport.Commands.CreateSport;

public class CreateSportCommand : IRequest<string>
{
    public required string Name { get; set; }
    public string? Icon { get; set; }
    public IFormFile? Image { get; set; }
}
