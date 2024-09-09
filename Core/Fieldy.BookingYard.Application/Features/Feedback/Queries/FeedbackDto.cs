using Fieldy.BookingYard.Domain.Enum;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries
{
	public class FeedbackDto
	{
		public int FeedbackID { get; set; }
		public Guid UserID { get; set; }
		public Guid FacilityID { get; set; }
		public string? Content { get; set; }
		public int Rating { get; set; }
		public TypeFeedback TypeFeedback { get; set; }
		public bool IsShow { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
