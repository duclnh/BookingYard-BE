using System;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;

public class FeedbackHomeDTO
{
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required string Content { get; set; }
    public int Rating { get; set; }
    public required string TypeFeedback { get; set; }
}
