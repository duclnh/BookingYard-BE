using AutoMapper;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Fieldy.BookingYard.Application.Features.RegisterPackage.Queries.GetAllRegisterPackage
{
    public class GetAllRegisterPackageHandler : IRequestHandler<GetAllRegisterPackageQuery, PagingResult<RegisterPackageDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRegisterPackageRepository _registerPackageRepository;

        public GetAllRegisterPackageHandler(IMapper mapper, IRegisterPackageRepository registerPackageRepository)
        {
            _mapper = mapper;
            _registerPackageRepository = registerPackageRepository;
        }

        public async Task<PagingResult<RegisterPackageDto>> Handle(GetAllRegisterPackageQuery request, CancellationToken cancellationToken)
        {
            var listRegisterPackage = await _registerPackageRepository.FindAllPaging(
                currentPage: request.requestParams.CurrentPage,
                pageSize: request.requestParams.PageSize,
                expression: string.IsNullOrEmpty(request.requestParams.Search) ? null : (x => (x.Facility != null && x.Facility.Name.ToLower().Trim().Contains(request.requestParams.Search.ToLower().Trim())) ||
								 (x.Package != null && x.Package.PackageName.ToLower().Trim().Contains(request.requestParams.Search.ToLower().Trim()))),
				orderBy: x => x.OrderByDescending(x => x.CreatedAt),
                cancellationToken: cancellationToken,
                includes: new Expression<Func<Domain.Entities.RegisterPackage, object>>[]
                {
                    x => x.Facility,
                    x => x.Package
                });

            return PagingResult<RegisterPackageDto>.Create(
               totalCount: listRegisterPackage.TotalCount,
               pageSize: listRegisterPackage.PageSize,
               currentPage: listRegisterPackage.CurrentPage,
               totalPages: listRegisterPackage.TotalPages,
               hasNext: listRegisterPackage.HasNext,
               hasPrevious: listRegisterPackage.HasPrevious,
               results: _mapper.Map<IList<RegisterPackageDto>>(listRegisterPackage.Results)
           );
        }
    }
}
