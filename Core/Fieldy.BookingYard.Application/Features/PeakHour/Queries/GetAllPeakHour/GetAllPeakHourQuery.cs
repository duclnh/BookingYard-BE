using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.PeakHour.Queries.GetAllPeakHour
{
	public record GetAllPeakHourQuery(RequestParams requestParams) : IRequest<PagingResult<PeakHourDto>>
	{
	}
}
