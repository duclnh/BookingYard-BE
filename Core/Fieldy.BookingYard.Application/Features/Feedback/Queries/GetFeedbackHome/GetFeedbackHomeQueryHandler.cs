using AutoMapper;
using Fieldy.BookingYard.Application.Features.Feedback.Queries.DTO;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.GetFeedbackHome;

public class GetFeedbackHomeQueryHandler : IRequestHandler<GetFeedbackHomeQuery, IList<FeedbackHomeDTO>>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IMapper _mapper;

    public GetFeedbackHomeQueryHandler(IFeedbackRepository feedbackRepository, IMapper mapper)
    {
        _feedbackRepository = feedbackRepository;
        _mapper = mapper;
    }

    public async Task<IList<FeedbackHomeDTO>> Handle(GetFeedbackHomeQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = new List<Domain.Entities.FeedBack>();
        
        feedbacks.AddRange(
          await _feedbackRepository.GetTopFeedBack(TypeFeedback.Owner, 3, cancellationToken)
        );

        int remainFeedback = 3 - feedbacks.Count;

        feedbacks.AddRange(
            await _feedbackRepository.GetTopFeedBack(TypeFeedback.Customer, remainFeedback + 3, cancellationToken)
        );

        return _mapper.Map<IList<FeedbackHomeDTO>>(feedbacks);
    }
}
