using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Features.Support.Queries.GetAllSupport;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Package.Queries
{
	public class GetAllPackageQueryHandler : IRequestHandler<GetAllPackageQuery, PagingResult<PackageDto>>
	{
		private readonly IMapper _mapper;
		private readonly IPackageRepository _packageRepository;

		public GetAllPackageQueryHandler(IMapper mapper, IPackageRepository packageRepository)
		{
			_mapper = mapper;
			_packageRepository = packageRepository;
		}

		public async Task<PagingResult<PackageDto>> Handle(GetAllPackageQuery request, CancellationToken cancellationToken)
		{
			var listSupport = await _packageRepository.FindAllPaging(
				requestParams: request.requestParams,
				expression: x => x.PackageName.ToLower().Contains(request.requestParams.Search.ToLower().Trim()),
				orderBy: x => x.OrderByDescending(x => x.CreateAt),
				cancellationToken: cancellationToken);

			return PagingResult<PackageDto>.Create(
			   totalCount: listSupport.TotalCount,
			   pageSize: listSupport.PageSize,
			   currentPage: listSupport.CurrentPage,
			   totalPages: listSupport.TotalPages,
			   hasNext: listSupport.HasNext,
			   hasPrevious: listSupport.HasPrevious,
			   results: _mapper.Map<IList<PackageDto>>(listSupport.Results)
		   );
		}
	}
}
