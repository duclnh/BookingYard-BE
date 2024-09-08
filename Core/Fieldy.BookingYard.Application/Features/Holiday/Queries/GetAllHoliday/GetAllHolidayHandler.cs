using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Holiday.Queries.GetAllHoliday
{
	public class GetAllHolidayHandler : IRequestHandler<GetAllHolidayQuery, PagingResult<HolidayDto>>
	{
		private readonly IMapper _mapper;
		private readonly IHolidayRepository _holidayRepository;

		public GetAllHolidayHandler(IMapper mapper, IHolidayRepository holidayRepository)
		{
			_mapper = mapper;
			_holidayRepository = holidayRepository;
		}

		public async Task<PagingResult<HolidayDto>> Handle(GetAllHolidayQuery request, CancellationToken cancellationToken)
		{
			var listholiday = await _holidayRepository.FindAllPaging(
				requestParams: request.requestParams,
				expression: null,
				orderBy: null,
				cancellationToken: cancellationToken);

			return PagingResult<HolidayDto>.Create(
			   totalCount: listholiday.TotalCount,
			   pageSize: listholiday.PageSize,
			   currentPage: listholiday.CurrentPage,
			   totalPages: listholiday.TotalPages,
			   hasNext: listholiday.HasNext,
			   hasPrevious: listholiday.HasPrevious,
			   results: _mapper.Map<IList<HolidayDto>>(listholiday.Results)
		   );
		}
	}
}
