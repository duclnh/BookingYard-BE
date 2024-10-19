using System;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;

public class FeedbackAdminDTO
{
    public int FeedbackID { get; set; }
    public required string Name { get; set; }

    public Guid? FacilityID { get; set; }
    public string? FacilityName { get; set; }
    public required string Phone { get; set; }
    public string? Content { get; set; }
    public int Rating { get; set; }
    public string? TypeFeedback { get; set; }
    public bool IsShow { get; set; }
    public List<string>? Images { get; set; }
    public DateTime CreatedAt { get; set; }
}
