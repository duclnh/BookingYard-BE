using AutoMapper;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
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
				currentPage: request.requestParams.CurrentPage,
				pageSize: request.requestParams.PageSize,
				expression: x => x.FacilityID == request.FaciliyId,
				orderBy: x => x.OrderByDescending(x => x.CreatedAt),
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
