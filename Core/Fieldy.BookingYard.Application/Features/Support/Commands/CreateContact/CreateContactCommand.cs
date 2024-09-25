using MediatR;

namespace Fieldy.BookingYard.Application.Features.Support.Commands.CreateContact
{
    public class CreateContactCommand : IRequest<string>
    {
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Content { get; set; }
    }
}
