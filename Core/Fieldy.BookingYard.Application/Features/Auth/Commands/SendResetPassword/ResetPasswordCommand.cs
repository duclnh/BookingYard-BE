using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.SendResetPassword{
    public class ResetPasswordCommand : IRequest<string>{
        public required string Email { get; set; }
    }
}