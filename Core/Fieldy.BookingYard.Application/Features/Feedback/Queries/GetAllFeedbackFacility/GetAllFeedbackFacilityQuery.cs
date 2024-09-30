using System;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedbackFacility;

public record class GetAllFeedbackFacilityQuery(RequestParams RequestParams, Guid FacilityID) : IRequest<PagingResult<FeedbackFacilityDetailDTO>>
{

}
