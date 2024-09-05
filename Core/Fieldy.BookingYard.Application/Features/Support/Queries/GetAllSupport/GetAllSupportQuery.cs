using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Support.Queries.GetAllSupport{
    public record GetAllSupportQuery(RequestParams requestParams) : IRequest<PagingResult<SupportDTO>>{

    }
}