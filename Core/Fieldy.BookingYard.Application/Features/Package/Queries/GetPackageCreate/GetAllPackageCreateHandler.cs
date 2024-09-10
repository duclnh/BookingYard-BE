using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Features.Package.Queries.GetPackageCreate;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Package.Queries.GetAllPackage
{
	public class GetAllPackageCreateHandler : IRequestHandler<GetPackageCreateQuery, IList<PackageCreate>>
	{
		private readonly IMapper _mapper;
		private readonly IPackageRepository _packageRepository;

		public GetAllPackageCreateHandler(IMapper mapper, IPackageRepository packageRepository)
		{
			_mapper = mapper;
			_packageRepository = packageRepository;
		}

		public async Task<IList<PackageCreate>> Handle(GetPackageCreateQuery request, CancellationToken cancellationToken)
		{
			var listPackage = await _packageRepository.FindAll(
				expression: x => x.IsDeleted == false,
				orderBy: x => x.OrderByDescending(x => x.CreatedAt),
				cancellationToken: cancellationToken);

			return _mapper.Map<IList<PackageCreate>>(listPackage);
		}
	}
}
