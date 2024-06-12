using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<string>
    {
        public required string UserID { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
