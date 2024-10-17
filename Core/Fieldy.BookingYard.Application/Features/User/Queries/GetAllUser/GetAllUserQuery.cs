using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.User.Queries{
    public record GetALlUserQuery(RequestParams requestParams) : IRequest<PagingResult<UserAdminDTO>>;
}