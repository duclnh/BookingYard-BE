namespace Fieldy.BookingYard.Application.Features.Dashboard.Queries.DTO
{
	public class DashboardCourtOwner
	{
		public decimal Revenue { get; set; }
		public int TotalBookings { get; set; }
		public int TotalBookingsCancel { get; set; }
		public RevenueDetail? DetailsRevenue { get; set; }
		public IList<SportCount>? CountBookings { get; set; }
	}
}
