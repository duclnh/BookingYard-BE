using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Package.Queries.GetAllPackage
{
	public class GetAllPackageHandler : IRequestHandler<GetAllPackageQuery, PagingResult<PackageDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPackageRepository _packageRepository;

        public GetAllPackageHandler(IMapper mapper, IPackageRepository packageRepository)
        {
            _mapper = mapper;
			_packageRepository = packageRepository;
        }

        public async Task<PagingResult<PackageDto>> Handle(GetAllPackageQuery request, CancellationToken cancellationToken)
        {
			var listPackage = await _packageRepository.FindAllPaging(
                requestParams: request.requestParams,
				expression: string.IsNullOrEmpty(request.requestParams.Search) ? null : (x => x.PackageName.ToLower().Contains(request.requestParams.Search.ToLower().Trim())),
                orderBy: x => x.OrderByDescending(x => x.CreatedAt),
                cancellationToken: cancellationToken);

			return PagingResult<PackageDto>.Create(
               totalCount: listPackage.TotalCount,
               pageSize: listPackage.PageSize,
               currentPage: listPackage.CurrentPage,
               totalPages: listPackage.TotalPages,
               hasNext: listPackage.HasNext,
               hasPrevious: listPackage.HasPrevious,
               results: _mapper.Map<IList<PackageDto>>(listPackage.Results)
           );
		}
	}
}
