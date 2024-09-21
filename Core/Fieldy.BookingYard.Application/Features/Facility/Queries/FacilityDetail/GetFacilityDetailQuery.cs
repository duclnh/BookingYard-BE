using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.FacilityDetail
{
	public record GetFacilityDetailQuery(Guid facilityID, CancellationToken cancellationToken) : IRequest<Domain.Entities.Facility>
	{
	}
}
