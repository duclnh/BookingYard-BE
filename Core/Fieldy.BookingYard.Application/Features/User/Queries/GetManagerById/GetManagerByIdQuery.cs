using Fieldy.BookingYard.Application.Features.User.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.User.Queries.GetManagerById
{
    public record GetManagerByIdQuery(Guid UserID) : IRequest<ManagerDTO>;
}