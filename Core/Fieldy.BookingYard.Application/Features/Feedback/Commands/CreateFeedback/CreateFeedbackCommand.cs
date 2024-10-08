using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback
{
	public class CreateFeedbackCommand : IRequest<string>
	{
		public Guid UserID { get; set; }
		public Guid BookingID { get; set; }
		public string? Image { get; set; }
		public string? Content { get; set; }
		public int Rating { get; set; }
	}
}
