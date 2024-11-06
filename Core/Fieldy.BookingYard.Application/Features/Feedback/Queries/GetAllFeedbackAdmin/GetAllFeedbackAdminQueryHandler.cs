using System;
using System.Linq.Expressions;
using AutoMapper;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedbackAdmin;

public class GetAllFeedbackAdminQueryHandler : IRequestHandler<GetAllFeedbackAdminQuery, PagingResult<FeedbackAdminDTO>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IMapper _mapper;

    public GetAllFeedbackAdminQueryHandler(IFeedbackRepository feedbackRepository, IMapper mapper)
    {
        _feedbackRepository = feedbackRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<FeedbackAdminDTO>> Handle(GetAllFeedbackAdminQuery request, CancellationToken cancellationToken)
    {
        List<Expression<Func<Domain.Entities.FeedBack, bool>>> expressions = new List<Expression<Func<Domain.Entities.FeedBack, bool>>>();

        if (!string.IsNullOrEmpty(request.RequestParams.Search))
        {
            string search = request.RequestParams.Search.Trim().ToLower();
            expressions.Add(
                x => x.User.Name.ToLower().Contains(search)
            );
        }
        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            expressions.Add(
                x => x.Rating.ToString() == request.OrderBy.ToLower().Trim()
            );
        }


        Expression<Func<Domain.Entities.FeedBack, bool>>[] expressionArray = expressions.ToArray();


        var feedbacks = await _feedbackRepository.FindAllPaging(
            currentPage: request.RequestParams.CurrentPage,
            pageSize: request.RequestParams.PageSize,
            expressions: expressionArray,
            orderBy: x => x.OrderByDescending(q => q.CreatedAt),
            cancellationToken: cancellationToken,
            x => x.Facility,
            x => x.User
        );

        return PagingResult<FeedbackAdminDTO>.Create(
            totalCount: feedbacks.TotalCount,
            pageSize: feedbacks.PageSize,
            currentPage: feedbacks.CurrentPage,
            totalPages: feedbacks.TotalPages,
            hasNext: feedbacks.HasNext,
            hasPrevious: feedbacks.HasPrevious,
            results: _mapper.Map<IList<FeedbackAdminDTO>>(feedbacks.Results)
        );
    }
}
