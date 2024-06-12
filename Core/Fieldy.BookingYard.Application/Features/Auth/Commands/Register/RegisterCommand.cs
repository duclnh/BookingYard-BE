using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.Register{
    public class RegisterCommand : IRequest<string>{
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}