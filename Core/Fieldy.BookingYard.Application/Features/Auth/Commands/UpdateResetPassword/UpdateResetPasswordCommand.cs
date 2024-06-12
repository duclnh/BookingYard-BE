using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.UpdateResetPassword
{
    public class UpdateResetPasswordCommand : IRequest<string>
    {
        public required string Email { get; set; }
        public required string VerificationCode { get; set; }
        public required string NewPassword { get; set; }
    }
}
