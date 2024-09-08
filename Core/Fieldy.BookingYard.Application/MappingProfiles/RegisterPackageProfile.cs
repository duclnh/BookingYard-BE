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
				.ForMember(dest => dest.RegisterPackageID, opt => opt.MapFrom(src => src.Id));
			CreateMap<CreateRegisterPackageCommand, RegisterPackage>();
			CreateMap<UpdateRegisterPackageCommand, RegisterPackage>();
		}
	}
}
