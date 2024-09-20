using System.Linq.Expressions;
using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.HistoryPoint.Queries.GetHistoryPoint
{
	public class GetHistoryPointQueryHandler : IRequestHandler<GetHistoryPointQuery, PagingResult<HistoryPointDto>>
	{
		private readonly IMapper _mapper;
		private readonly IHistoryPointRepository _HistoryPointRepository;

		public GetHistoryPointQueryHandler(IMapper mapper, IHistoryPointRepository HistoryPointRepository)
		{
			_mapper = mapper;
			_HistoryPointRepository = HistoryPointRepository;
		}

		public async Task<PagingResult<HistoryPointDto>> Handle(GetHistoryPointQuery request, CancellationToken cancellationToken)
		{
			Expression<Func<Domain.Entities.HistoryPoint, bool>> expression;

			expression = request.type switch
			{
				"all" => e => e.UserID == request.UserID,
				"positive" => e => e.UserID == request.UserID && e.Point > 0,
				"negative" => e => e.UserID == request.UserID && e.Point < 0,
			};

			var listHistoryPoint = await _HistoryPointRepository.FindAllPaging(
				requestParams: request.requestParams,
				expression: expression,
				orderBy: x => x.OrderByDescending(x => x.CreatedAt),
				cancellationToken: cancellationToken);

			return PagingResult<HistoryPointDto>.Create(
			   totalCount: listHistoryPoint.TotalCount,
			   pageSize: listHistoryPoint.PageSize,
			   currentPage: listHistoryPoint.CurrentPage,
			   totalPages: listHistoryPoint.TotalPages,
			   hasNext: listHistoryPoint.HasNext,
			   hasPrevious: listHistoryPoint.HasPrevious,
			   results: _mapper.Map<IList<HistoryPointDto>>(listHistoryPoint.Results)
		   );
		}
	}
}
