using Fieldy.BookingYard.Domain.Enum;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.UpdateFeedback
{
	public class 	UpdateFeedbackCommand : IRequest<string>
	{
		public int FeedbackID { get; set; }
		public string? Image { get; set; }
		public string? Content { get; set; }
		public int Rating { get; set; }
		public TypeFeedback TypeFeedback { get; set; }
		public bool IsShow { get; set; }
	}
}
