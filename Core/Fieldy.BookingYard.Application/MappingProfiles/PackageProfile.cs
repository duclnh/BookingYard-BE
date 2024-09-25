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
			CreateMap<Package, PackageCreate>()
				.ForMember(dest => dest.PackageId, opt => opt.MapFrom(src => src.Id));
			CreateMap<CreatePackageCommand, Package>();
			/*CreateMap<UpdatePackageCommand, Package>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PackageId))
				.ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
				.ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
				.ForMember(dest => dest.ModifiedAt, opt => opt.Ignore());*/
		}
	}
}
