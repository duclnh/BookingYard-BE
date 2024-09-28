using System;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedBackFacility;

public record class GetFeedBackFacilityQuery(RequestParams RequestParams, Guid FacilityID) : IRequest<PagingResult<FeedbackFacilityDetailDTO>>
{

}
