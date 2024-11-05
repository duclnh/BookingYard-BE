using AutoMapper;
using Fieldy.BookingYard.Application.Features.Facility.Commands.CreateFacility;
using Fieldy.BookingYard.Application.Features.Facility.Queries;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
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

        CreateMap<Facility, FacilityAdminDTO>()
            .ForMember(x => x.FacilityID, q => q.MapFrom(x => x.Id))
            .ForMember(x => x.Address, q => q.MapFrom(x => x.FullAddress))
            .ForMember(x => x.OwnerName, q => q.MapFrom(x => x.User.Name))
            .ForMember(x => x.FacilityName, q => q.MapFrom(x => x.Name));

        CreateMap<Facility, FacilityCustomerDTO>()
            .ForMember(x => x.FacilityImage, q => q.MapFrom(x => x.Image))
            .ForMember(x => x.FacilityName, q => q.MapFrom(x => x.Name))
            .ForMember(x => x.FacilityAddress, q => q.MapFrom(x => x.FullAddress))
            .ForMember(x => x.FacilityID, q => q.MapFrom(x => x.Id));

        CreateMap<Facility, FacilityDetailDTO>()
            .ForMember(x => x.FacilityName, q => q.MapFrom(x => x.Name))
            .ForMember(x => x.FacilityAddress, q => q.MapFrom(x => x.FullAddress))
            .ForMember(x => x.FacilityID, q => q.MapFrom(x => x.Id))
            .ForMember(dest => dest.StartTime,
                   opt => opt.MapFrom(src => src.StartTime.Equals(new TimeSpan(0, 0, 0)) ? "24:00" : src.StartTime.ToString(@"hh\:mm")))
            .ForMember(dest => dest.EndTime,
                   opt => opt.MapFrom(src => src.EndTime.Equals(new TimeSpan(0, 0, 0)) ? "24:00" : src.EndTime.ToString(@"hh\:mm")));

        CreateMap<Facility, FacilityHomeDTO>()
           .ForMember(x => x.FacilityImage, q => q.MapFrom(x => x.Image))
           .ForMember(x => x.FacilityName, q => q.MapFrom(x => x.Name))
           .ForMember(x => x.FacilityAddress, q => q.MapFrom(x => x.FullAddress))
           .ForMember(x => x.FacilityID, q => q.MapFrom(x => x.Id))
           .ForMember(dest => dest.StartTime,
                   opt => opt.MapFrom(src => src.StartTime.Equals(new TimeSpan(0, 0, 0)) ? "24:00" : src.StartTime.ToString(@"hh\:mm")))
            .ForMember(dest => dest.EndTime,
                   opt => opt.MapFrom(src => src.EndTime.Equals(new TimeSpan(0, 0, 0)) ? "24:00" : src.EndTime.ToString(@"hh\:mm")));

        CreateMap<Facility, FacilityPositionDTO>();

    }
}
