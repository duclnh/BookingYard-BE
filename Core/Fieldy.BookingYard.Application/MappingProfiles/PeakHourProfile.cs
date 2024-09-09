using AutoMapper;
using Fieldy.BookingYard.Application.Features.PeakHour.Commands.CreatePeakHour;
using Fieldy.BookingYard.Application.Features.PeakHour.Commands.DeletePeakHour;
using Fieldy.BookingYard.Application.Features.PeakHour.Commands.UpdatePeakHour;
using Fieldy.BookingYard.Application.Features.PeakHour.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	public class PeakHourProfile : Profile
	{
		public PeakHourProfile()
		{
			CreateMap<PeakHour, PeakHourDto>()
				.ForMember(dest => dest.PeakHourID, opt => opt.MapFrom(src => src.Id));
			CreateMap<CreatePeakHourCommand, PeakHour>();
			CreateMap<UpdatePeakHourCommand, PeakHour>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PeakHourID));
			CreateMap<DeletePeakHourCommand, PeakHour>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PeakHourID));
		}
	}
}
