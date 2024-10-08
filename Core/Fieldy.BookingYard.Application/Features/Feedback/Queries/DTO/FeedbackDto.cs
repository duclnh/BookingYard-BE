﻿using Fieldy.BookingYard.Domain.Enums;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries
{
	public class FeedbackDto
	{
		public int FeedbackID { get; set; }
		public required string Name { get; set; }
		public required string Phone { get; set; }
		public string? Content { get; set; }
		public int Rating { get; set; }
		public TypeFeedback TypeFeedback { get; set; }
		public bool IsShow { get; set; }
		public List<string>? Images { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
