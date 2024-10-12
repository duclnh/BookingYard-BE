using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Dashboard.Queries
{
	public record GetRevenueQuery(string typeTimeBased, CancellationToken cancellationToken) : IRequest<DashboardHome>
	{
	}
}
