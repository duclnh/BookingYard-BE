using AutoMapper;
using Fieldy.BookingYard.Application.Features.RegisterPackage.Commands.CreateRegisterPackage;
using Fieldy.BookingYard.Application.Features.RegisterPackage.Commands.UpdateRegisterPackage;
using Fieldy.BookingYard.Application.Features.RegisterPackage.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	public class RegisterPackageProfile : Profile
	{
		public RegisterPackageProfile()
		{
			CreateMap<RegisterPackage, RegisterPackageDto>()
				.ForMember(dest => dest.RegisterPackageID, opt => opt.MapFrom(src => src.RegisterPackageID))
				.ForMember(dest => dest.PackageID, opt => opt.MapFrom(src => src.PackageID))
				.ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
				.ForMember(dest => dest.FacilityID, opt => opt.MapFrom(src => src.FacilityID))
				.ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => src.RegisterDate))
				.ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom(src => src.ExpiredDate));
			CreateMap<CreateRegisterPackageCommand, RegisterPackage>()
				.ForMember(dest => dest.PackageID, opt => opt.MapFrom(src => src.PackageID))
				.ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
				.ForMember(dest => dest.FacilityID, opt => opt.MapFrom(src => src.FacilityID))
				.ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => src.RegisterDate))
				.ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom(src => src.ExpiredDate));
			CreateMap<UpdateRegisterPackageCommand, RegisterPackage>()
				.ForMember(dest => dest.PackageID, opt => opt.MapFrom(src => src.PackageID))
				.ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
				.ForMember(dest => dest.FacilityID, opt => opt.MapFrom(src => src.FacilityID))
				.ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => src.RegisterDate))
				.ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom(src => src.ExpiredDate));
		}
	}
}
