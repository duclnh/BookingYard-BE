using MediatR;

namespace Fieldy.BookingYard.Application.Features.Dashboard.Queries
{
	public class GetRevenueQuery : IRequest<DashboardHome>
	{
		public string? TypeTimeBased { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
	}
}
