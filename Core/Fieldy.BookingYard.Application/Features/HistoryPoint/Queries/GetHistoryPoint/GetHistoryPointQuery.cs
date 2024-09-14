using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.HistoryPoint.Queries.GetHistoryPoint
{
    public record GetHistoryPointQuery(RequestParams requestParams, Guid UserID) : IRequest<PagingResult<HistoryPointDto>>
    {
    }
}
