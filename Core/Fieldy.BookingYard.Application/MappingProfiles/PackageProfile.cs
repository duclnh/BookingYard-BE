using AutoMapper;
using Fieldy.BookingYard.Application.Features.Package.Commands.CreatePackage;
using Fieldy.BookingYard.Application.Features.Package.Commands.UpdatePackage;
using Fieldy.BookingYard.Application.Features.Package.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
    public class PackageProfile : Profile
	{
		public PackageProfile()
		{
			CreateMap<Package, PackageDto>()
				.ForMember(dest => dest.PackageId, opt => opt.MapFrom(src => src.Id));
			CreateMap<CreatePackageCommand, Package>();
			CreateMap<UpdatePackageCommand, Package>();
		}
	}
}
