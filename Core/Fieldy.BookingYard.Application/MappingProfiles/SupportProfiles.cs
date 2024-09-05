using AutoMapper;
using Fieldy.BookingYard.Application.Features.Support.Commands.CreateAdvise;
using Fieldy.BookingYard.Application.Features.Support.Queries.GetAllSupport;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles
{
    public class SupportProfiles : Profile
    {
        public SupportProfiles()
        {
            CreateMap<CreateAdviseCommand, Support>();
            CreateMap<Support, SupportDTO>();
        }
    }
}
