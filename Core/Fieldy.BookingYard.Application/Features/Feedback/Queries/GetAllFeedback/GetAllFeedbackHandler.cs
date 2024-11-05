using System.Linq.Expressions;
using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Queries.GetAllFeedback
{
	public class GetAllFeedbackHandler : IRequestHandler<GetAllFeedbackQuery, PagingResult<FeedbackDto>>
	{
		private readonly IMapper _mapper;
		private readonly IFeedbackRepository _FeedbackRepository;
		private readonly IJWTService _jWTService;
		private readonly IUserRepository _userRepository;

		public GetAllFeedbackHandler(IMapper mapper, IFeedbackRepository feedbackRepository, IJWTService jWTService, IUserRepository userRepository)
		{
			_mapper = mapper;
			_FeedbackRepository = feedbackRepository;
			_jWTService = jWTService;
			_userRepository = userRepository;
		}

		public async Task<PagingResult<FeedbackDto>> Handle(GetAllFeedbackQuery request, CancellationToken cancellationToken)
		{
			var user = await _userRepository.Find(x => x.Id == _jWTService.UserID, cancellationToken);

			var listFeedback = await _FeedbackRepository.FindAllPaging(
				currentPage: request.requestParams.CurrentPage,
				pageSize: request.requestParams.PageSize,
				expression: x => x.FacilityID == request.facilityID && x.TypeFeedback == TypeFeedback.Customer,
				orderBy: x => x.OrderByDescending(x => x.CreatedAt),
				cancellationToken: cancellationToken,
				x => x.Images,
				x => x.User
			);

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
