using AutoMapper;
using Fieldy.BookingYard.Application.Features.HistoryPoint.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
	public class HistoryPointProfile : Profile
	{
		public HistoryPointProfile() =>
			CreateMap<HistoryPoint, HistoryPointDto>()
				.ForMember(dest => dest.HistoryPointID, opt => opt.MapFrom(src => src.Id));
	}
}
