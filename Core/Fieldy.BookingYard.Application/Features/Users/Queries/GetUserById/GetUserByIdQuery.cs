using MediatR;

namespace Fieldy.BookingYard.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(string UserID) : IRequest<UserDTO>;
}