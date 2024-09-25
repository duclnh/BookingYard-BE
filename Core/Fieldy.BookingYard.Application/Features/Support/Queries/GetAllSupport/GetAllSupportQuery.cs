using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Support.Queries.GetAllSupport{
    public record GetAllSupportQuery(RequestParams requestParams,TypeSupport? typeSupport) : IRequest<PagingResult<SupportDTO>>{

    }
}