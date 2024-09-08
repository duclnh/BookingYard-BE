using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedback
{
	public record GetAllFeedbackQuery(RequestParams requestParams) : IRequest<PagingResult<FeedbackDto>>
	{
	}
}
