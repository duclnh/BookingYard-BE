using Fieldy.BookingYard.Domain.Enum;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback
{
	public class CreateFeedbackCommand : IRequest<string>
	{
		public Guid UserID { get; set; }
		public Guid FacilityID { get; set; }
		public string? Image { get; set; }
		public string? Content { get; set; }
		public int Rating { get; set; }
		public TypeFeedback TypeFeedback { get; set; }
		public bool IsShow { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
