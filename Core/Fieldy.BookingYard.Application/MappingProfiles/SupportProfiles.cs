using AutoMapper;
using Fieldy.BookingYard.Application.Features.Support.Commands.CreateAdvise;
using Fieldy.BookingYard.Application.Features.Support.Commands.CreateContact;
using Fieldy.BookingYard.Application.Features.Support.Queries.GetAllSupport;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
    public class SupportProfiles : Profile
    {
        public SupportProfiles()
        {
            CreateMap<CreateAdviseCommand, Support>();
            CreateMap<CreateContactCommand, Support>();
            CreateMap<Support, SupportDTO>()
                .ForMember(d => d.CreatedAt, d => d.MapFrom(e => e.CreatedAt.ToString("dd-MM-yyyy HH:ss:mm")))
                .ForMember(d => d.ModifiedAt, d => d.MapFrom(e => e.ModifiedAt != null ? e.ModifiedAt.Value.ToString("dd-MM-yyyy HH:ss:mm") : null));
        }
    }
}
