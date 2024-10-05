using AutoMapper;
using Fieldy.BookingYard.Application.Features.Court;
using Fieldy.BookingYard.Application.Features.Court.Commands.CreateCourt;
using Fieldy.BookingYard.Application.Features.Court.Queries.DTO;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles;

public class CourtProfile : Profile
{
    public CourtProfile()
    {
        CreateMap<CreateCourtCommand, Court>();
        CreateMap<Court, CourtDTO>()
               .ForMember(x => x.CourtID, q => q.MapFrom(x => x.Id))
               .ForMember(x => x.SportID, q => q.MapFrom(x => x.Sport != null ? x.Sport.Id : 0))
               .ForMember(x => x.SportName, q => q.MapFrom(x => x.Sport != null ? x.Sport.SportName : null));

        CreateMap<Court, CourtDetailDTO>()
              .ForMember(x => x.CourtID, q => q.MapFrom(x => x.Id))
              .ForMember(x => x.SportID, q => q.MapFrom(x => x.Sport != null ? x.Sport.Id : 0))
              .ForMember(x => x.SportName, q => q.MapFrom(x => x.Sport != null ? x.Sport.SportName : null));

        CreateMap<Court, CourtBookingDTO>()
                .ForMember(x => x.CourtID, q => q.MapFrom(x => x.Id));
    }

}
