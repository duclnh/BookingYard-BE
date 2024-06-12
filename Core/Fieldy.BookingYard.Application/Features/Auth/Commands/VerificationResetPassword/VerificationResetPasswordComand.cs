using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.VerificationResetPassword
{
    public class VerificationResetPasswordCommand : IRequest<string>
    {
        public required string Email { get; set; }
        public required string VerificationCode { get; set; }
    }
}
