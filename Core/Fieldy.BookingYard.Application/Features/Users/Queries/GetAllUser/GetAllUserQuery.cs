using Fieldy.BookingYard.Application.Features.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Users.Queries{
    public record GetALlUserQuery(RequestParams requestParams) : IRequest<PagingResult<UserDTO>>;
}