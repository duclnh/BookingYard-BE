using AutoMapper;
using Fieldy.BookingYard.Application.Features.FacilityTime.Commands.CreateFacilityTime;
using Fieldy.BookingYard.Application.Features.FacilityTime.Commands.DeleteFacilityTime;
using Fieldy.BookingYard.Application.Features.FacilityTime.Commands.UpdateFacilityTime;
using Fieldy.BookingYard.Application.Features.FacilityTime.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	public class FacilityTimeProfile : Profile
	{
		public FacilityTimeProfile()
		{
			CreateMap<FacilityTime, FacilityTimeDto>()
				.ForMember(dest => dest.FacilityTimeID, opt => opt.MapFrom(src => src.Id));
			CreateMap<CreateFacilityTimeCommand, FacilityTime>();
			CreateMap<UpdateFacilityTimeCommand, FacilityTime>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FacilityTimeID));
			CreateMap<DeleteFacilityTimeCommand, FacilityTime>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FacilityTimeID));
		}
	}
}
