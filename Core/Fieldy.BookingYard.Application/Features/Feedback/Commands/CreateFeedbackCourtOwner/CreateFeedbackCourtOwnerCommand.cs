using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedbackCourtOwner;

public class CreateFeedbackCourtOwnerCommand : IRequest<string>
{
    public Guid UserID { get; set; }
    public string? Content { get; set; }
    public int Rating { get; set; }
}
