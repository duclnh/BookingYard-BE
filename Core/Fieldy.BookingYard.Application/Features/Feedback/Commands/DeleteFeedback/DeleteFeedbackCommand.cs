using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.DeleteFeedback
{
	public class DeleteFeedbackCommand : IRequest<string>
	{
		public int FeedbackID { get; set; }
	}
}
