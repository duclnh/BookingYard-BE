using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Discount.Queries.GetAllDiscount
{
	public record GetAllDiscountQuery(RequestParams requestParams) : IRequest<PagingResult<DiscountDto>>
	{
	}
}
