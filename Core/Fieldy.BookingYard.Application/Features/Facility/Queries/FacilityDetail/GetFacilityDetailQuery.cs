using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.FacilityDetail
{
	public record GetFacilityDetailQuery(Guid facilityID, CancellationToken cancellationToken) : IRequest<FacilityDetailDTO>
	{
	}
}
