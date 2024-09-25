using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.SendVerificationCode{
    public class SendVerificationCodeCommand : IRequest<string>{
        public required Guid UserID { get; set; }
    }
}