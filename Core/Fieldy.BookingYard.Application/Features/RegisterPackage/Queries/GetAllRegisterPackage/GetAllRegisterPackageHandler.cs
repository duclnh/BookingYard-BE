using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Features.Package.Queries;
using Fieldy.BookingYard.Application.Features.Package.Queries.GetAllPackage;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				requestParams: request.requestParams,
				expression: x => x.Facility.FacilityName.ToLower().Contains(request.requestParams.Search.ToLower().Trim())
								|| x.Package.PackageName.ToLower().Contains(request.requestParams.Search.ToLower().Trim()),
				orderBy: x => x.OrderByDescending(x => x.CreatedAt),
				cancellationToken: cancellationToken);

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
