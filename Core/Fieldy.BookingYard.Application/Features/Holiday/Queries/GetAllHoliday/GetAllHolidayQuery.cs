using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Holiday.Queries.GetAllHoliday
{
	public record GetAllHolidayQuery(RequestParams requestParams) : IRequest<PagingResult<HolidayDto>>
	{
	}
}
