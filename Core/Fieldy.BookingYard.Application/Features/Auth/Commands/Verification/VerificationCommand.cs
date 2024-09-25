using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.Verification
{
    public class VerificationCommand : IRequest<string>
    {
        public required Guid UserID { get; set; }
        public required string VerificationCode { get; set; }
    }
}
