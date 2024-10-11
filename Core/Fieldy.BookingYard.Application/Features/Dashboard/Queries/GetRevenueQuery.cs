using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Dashboard.Queries
{
	public record GetRevenueQuery(TypeTimeBased typeTimeBased, TypeBooking typeBooking, CancellationToken cancellationToken) : IRequest<DashboardHome>
	{
	}
}
