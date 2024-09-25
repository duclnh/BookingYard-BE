using AutoMapper;
using Fieldy.BookingYard.Application.Features.Sport.Commands.CreateSport;
using Fieldy.BookingYard.Application.Features.Sport.Queries.DTO;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles;

public class SportProfile : Profile
{
    public SportProfile()
    {
        CreateMap<CreateSportCommand, Sport>();

        CreateMap<Sport, SportCreateDTO>()
            .ForMember(x => x.SportID, q => q.MapFrom(x => x.Id));
    }
}
