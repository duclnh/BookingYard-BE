using AutoMapper;
using Fieldy.BookingYard.Application.Features.Facility.Commands.CreateFacility;
using Fieldy.BookingYard.Application.Features.Facility.Queries;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles;

public class FacilityProfile : Profile
{
    public FacilityProfile()
    {
        CreateMap<CreateFacilityCommand, User>()
            .ForMember(x => x.Email, q => q.MapFrom(x => x.Email))
            .ForMember(x => x.Phone, q => q.MapFrom(x => x.Phone))
            .ForMember(x => x.Name, q => q.MapFrom(x => x.Name))
            .ForMember(x => x.Address, q => q.Ignore());
        CreateMap<CreateFacilityCommand, Facility>()
            .ForMember(x => x.Address, q => q.MapFrom(x => x.Address))
            .ForMember(x => x.Name, q => q.MapFrom(x => x.FacilityName))
            .ForMember(x => x.Image, q => q.Ignore())
            .ForMember(x => x.Logo, q => q.Ignore());

        CreateMap<Facility, FacilityAdminDTO>();
    }
}
