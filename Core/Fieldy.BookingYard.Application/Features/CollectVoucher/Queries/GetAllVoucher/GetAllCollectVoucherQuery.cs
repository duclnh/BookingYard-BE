using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.CollectVoucher.Queries.GetAllVoucher
{
	public record GetAllCollectVoucherQuery(RequestParams requestParams, Guid UserID, string? type) : IRequest<PagingResult<CollectVoucherDto>>
	{
	}
}
