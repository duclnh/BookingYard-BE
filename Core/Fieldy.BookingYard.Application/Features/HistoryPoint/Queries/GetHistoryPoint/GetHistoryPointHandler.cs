using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.HistoryPoint.Queries.GetHistoryPoint
{
	public class GetHistoryPointHandler : IRequestHandler<GetHistoryPointQuery, PagingResult<HistoryPointDto>>
	{
		private readonly IMapper _mapper;
		private readonly IHistoryPointRepository _HistoryPointRepository;

		public GetHistoryPointHandler(IMapper mapper, IHistoryPointRepository HistoryPointRepository)
		{
			_mapper = mapper;
			_HistoryPointRepository = HistoryPointRepository;
		}

		public async Task<PagingResult<HistoryPointDto>> Handle(GetHistoryPointQuery request, CancellationToken cancellationToken)
		{
			var listHistoryPoint = await _HistoryPointRepository.FindAllPaging(
				requestParams: request.requestParams,
				expression: x => x.UserID == request.UserID,
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
