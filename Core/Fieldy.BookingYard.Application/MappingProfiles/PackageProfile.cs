using AutoMapper;
using Fieldy.BookingYard.Application.Features.Package;
using Fieldy.BookingYard.Application.Features.Package.Commands.CreatePackage;
using Fieldy.BookingYard.Application.Features.Package.Commands.UpdatePackage;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	public class PackageProfile : Profile
	{
		public PackageProfile()
		{
			CreateMap<Package, PackageDto>();
			CreateMap<CreatePackageCommand, Package>();
			CreateMap<UpdatePackageCommand, Package>();
		}
	}
}
