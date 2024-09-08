using AutoMapper;
using Fieldy.BookingYard.Application.Features.Holiday.Commands.CreateHoliday;
using Fieldy.BookingYard.Application.Features.Holiday.Commands.DeleteHoliday;
using Fieldy.BookingYard.Application.Features.Holiday.Commands.UpdateHoliday;
using Fieldy.BookingYard.Application.Features.Holiday.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	public class HolidayProfile : Profile
	{
		public HolidayProfile()
		{
			CreateMap<Holiday, HolidayDto>()
				.ForMember(dest => dest.HolidayID, opt => opt.MapFrom(src => src.Id));
			CreateMap<CreateHolidayCommand, Holiday>();
			CreateMap<UpdateHolidayCommand, Holiday>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.HolidayID));
			CreateMap<DeleteHolidayCommand, Holiday>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.HolidayID));
		}
	}
}
