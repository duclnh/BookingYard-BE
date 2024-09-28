using AutoMapper;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedBackFacility;

public class GetFeedBackFacilityQueryHandler : IRequestHandler<GetFeedBackFacilityQuery, PagingResult<FeedbackFacilityDetailDTO>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IMapper _mapper;

    public GetFeedBackFacilityQueryHandler(IFeedbackRepository feedbackRepository, IMapper mapper)
    {
        _feedbackRepository = feedbackRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<FeedbackFacilityDetailDTO>> Handle(GetFeedBackFacilityQuery request, CancellationToken cancellationToken)
    {
        var feedbackFacilities = await _feedbackRepository.FindAllPaging(
            currentPage: request.RequestParams.CurrentPage,
            pageSize: request.RequestParams.PageSize,
            expression: x => x.FacilityID == request.FacilityID
                            && x.IsShow == true,
            orderBy: x => x.OrderByDescending(q => q.CreatedAt),
            cancellationToken: cancellationToken,
            x => x.User,
            x => x.Images
        );

        var feedbackFacilitiesMapped = _mapper.Map<IList<FeedbackFacilityDetailDTO>>(feedbackFacilities.Results);

        return PagingResult<FeedbackFacilityDetailDTO>.Create(
            totalCount: feedbackFacilities.TotalCount,
            pageSize: feedbackFacilities.PageSize,
            currentPage: feedbackFacilities.CurrentPage,
            totalPages: feedbackFacilities.TotalPages,
            hasNext: feedbackFacilities.HasNext,
            hasPrevious: feedbackFacilities.HasPrevious,
            results: feedbackFacilitiesMapped
        );
    }
}
