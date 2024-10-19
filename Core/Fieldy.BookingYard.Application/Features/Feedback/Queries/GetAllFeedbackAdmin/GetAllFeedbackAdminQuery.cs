using System;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedbackAdmin;

public record class GetAllFeedbackAdminQuery(RequestParams RequestParams, string? OrderBy) : IRequest<PagingResult<FeedbackAdminDTO>>
{

}
