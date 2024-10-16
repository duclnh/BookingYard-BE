using Fieldy.BookingYard.Application.Features.Dashboard.Queries.DTO;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Dashboard.Queries.GetRevenueCourtOwner
{
	public record GetRevenueCourtOwnerQuery(string typeTimeBased, Guid facilityId, CancellationToken cancellationToken) : IRequest<DashboardCourtOwner>
	{
	}
}
