using MediatR;

namespace Fieldy.BookingYard.Application.Features.Support.Commands.UpdateSupport
{
    public class UpdateSupportCommand : IRequest<string>
    {
        public int SupportID { get; set; }
        public required string Note { get; set;}
    }
}
