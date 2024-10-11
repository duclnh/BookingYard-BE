namespace Fieldy.BookingYard.Application.Features.Dashboard
{
	public class DashboardHome
	{
		public decimal Revenue { get; set; }
		public int TotalBookings { get; set; }
		public int TotalBookingsCancel { get; set; }
		public IList<decimal>? DetailsRevenue { get; set; }
		public IList<SportCount>? CountBookings { get; set; }
	}

	public class SportCount
	{
		public required string SportName { get; set; }
		public int Count { get; set; }
	}
}
