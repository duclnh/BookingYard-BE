using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedback
{
	public class GetAllFeedbackHandler : IRequestHandler<GetAllFeedbackQuery, PagingResult<FeedbackDto>>
	{
		private readonly IMapper _mapper;
		private readonly IFeedbackRepository _FeedbackRepository;

		public GetAllFeedbackHandler(IMapper mapper, IFeedbackRepository FeedbackRepository)
		{
			_mapper = mapper;
			_FeedbackRepository = FeedbackRepository;
		}

		public async Task<PagingResult<FeedbackDto>> Handle(GetAllFeedbackQuery request, CancellationToken cancellationToken)
		{
			var listFeedback = await _FeedbackRepository.FindAllPaging(
				requestParams: request.requestParams,
				expression: null,
				orderBy: null,
				cancellationToken: cancellationToken);

			return PagingResult<FeedbackDto>.Create(
			   totalCount: listFeedback.TotalCount,
			   pageSize: listFeedback.PageSize,
			   currentPage: listFeedback.CurrentPage,
			   totalPages: listFeedback.TotalPages,
			   hasNext: listFeedback.HasNext,
			   hasPrevious: listFeedback.HasPrevious,
			   results: _mapper.Map<IList<FeedbackDto>>(listFeedback.Results)
		   );
		}
	}
}
