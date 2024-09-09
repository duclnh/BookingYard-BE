using Fieldy.BookingYard.Application.Models.User;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.User.Queries.GetUserUpdateById
{
    public record GetUserUpdateById(Guid UserID) : IRequest<UserUpdateDTO>;
}