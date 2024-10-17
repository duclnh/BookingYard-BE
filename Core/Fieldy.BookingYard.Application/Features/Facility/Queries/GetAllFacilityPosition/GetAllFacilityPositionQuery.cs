using System;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.GetAllFacilityPosition;

public record class GetAllFacilityPositionQuery : IRequest<IList<FacilityPositionDTO>>
{

}
