using System;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.GetFacilityHome;

public record class GetFacilityHomeQuery() : IRequest<IList<FacilityHomeDTO>>
{

}
