using System;
using AutoMapper;
using Fieldy.BookingYard.Application.Features.Court.Commands.CreateCourt;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.MappingProfiles;

public class CourtProfile : Profile
{
    public CourtProfile()
    {
        CreateMap<CreateCourtCommand, Court>();
    }
}
