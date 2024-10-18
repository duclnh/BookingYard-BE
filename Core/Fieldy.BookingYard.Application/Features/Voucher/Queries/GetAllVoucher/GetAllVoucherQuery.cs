using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.GetAllVoucher
{
	public record GetAllVoucherQuery(RequestParams requestParams, string? OrderBy) : IRequest<PagingResult<VoucherDTO>>
	{
	}
}
