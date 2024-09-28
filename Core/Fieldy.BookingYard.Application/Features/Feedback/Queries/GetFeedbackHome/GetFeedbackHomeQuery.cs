using System;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.GetFeedbackHome;

public record class GetFeedbackHomeQuery : IRequest<IList<FeedbackHomeDTO>>
{

}
