using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.FacilityTime.Queries.GetAllFacilityTime
{
	public class GetAllFacilityTimeHandler : IRequestHandler<GetAllFacilityTimeQuery, PagingResult<FacilityTimeDto>>
	{
		private readonly IMapper _mapper;
		private readonly IFacilityTimeRepository _FacilityTimeRepository;

		public GetAllFacilityTimeHandler(IMapper mapper, IFacilityTimeRepository FacilityTimeRepository)
		{
			_mapper = mapper;
			_FacilityTimeRepository = FacilityTimeRepository;
		}

		public async Task<PagingResult<FacilityTimeDto>> Handle(GetAllFacilityTimeQuery request, CancellationToken cancellationToken)
		{
			var listFacilityTime = await _FacilityTimeRepository.FindAllPaging(
				requestParams: request.requestParams,
				expression: null,
				orderBy: null,
				cancellationToken: cancellationToken);

			return PagingResult<FacilityTimeDto>.Create(
			   totalCount: listFacilityTime.TotalCount,
			   pageSize: listFacilityTime.PageSize,
			   currentPage: listFacilityTime.CurrentPage,
			   totalPages: listFacilityTime.TotalPages,
			   hasNext: listFacilityTime.HasNext,
			   hasPrevious: listFacilityTime.HasPrevious,
			   results: _mapper.Map<IList<FacilityTimeDto>>(listFacilityTime.Results)
		   );
		}
	}
}
