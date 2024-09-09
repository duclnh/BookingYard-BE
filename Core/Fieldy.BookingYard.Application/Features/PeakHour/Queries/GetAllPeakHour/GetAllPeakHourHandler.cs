using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.PeakHour.Queries.GetAllPeakHour
{
	public class GetAllPeakHourHandler : IRequestHandler<GetAllPeakHourQuery, PagingResult<PeakHourDto>>
	{
		private readonly IMapper _mapper;
		private readonly IPeakHourRepository _PeakHourRepository;

		public GetAllPeakHourHandler(IMapper mapper, IPeakHourRepository PeakHourRepository)
		{
			_mapper = mapper;
			_PeakHourRepository = PeakHourRepository;
		}

		public async Task<PagingResult<PeakHourDto>> Handle(GetAllPeakHourQuery request, CancellationToken cancellationToken)
		{
			var listPeakHour = await _PeakHourRepository.FindAllPaging(
				requestParams: request.requestParams,
				expression: null,
				orderBy: null,
				cancellationToken: cancellationToken);

			return PagingResult<PeakHourDto>.Create(
			   totalCount: listPeakHour.TotalCount,
			   pageSize: listPeakHour.PageSize,
			   currentPage: listPeakHour.CurrentPage,
			   totalPages: listPeakHour.TotalPages,
			   hasNext: listPeakHour.HasNext,
			   hasPrevious: listPeakHour.HasPrevious,
			   results: _mapper.Map<IList<PeakHourDto>>(listPeakHour.Results)
		   );
		}
	}
}
