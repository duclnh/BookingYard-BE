using MediatR;

namespace Fieldy.BookingYard.Application.Features.User.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid UserID) : IRequest<UserDTO>;
}