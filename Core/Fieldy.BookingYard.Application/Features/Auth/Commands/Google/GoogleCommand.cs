using Fieldy.BookingYard.Application.Models.Auth;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.Google
{
    public class GoogleCommand : IRequest<AuthResponse>
    {
        public required string GoogleID { get; set;}
        public required string Name { get; set;}
        public required string ImageUrl { get; set; }
        public required string Email { get; set; }
    }
}