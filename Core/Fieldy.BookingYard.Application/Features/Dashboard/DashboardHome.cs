using Fieldy.BookingYard.Domain.Enums;
namespace Fieldy.BookingYard.Application.Features.Dashboard
{
	public class DashboardHome
	{
		public decimal Revenue { get; set; }
		public int TotalBookings { get; set; }
		public int TotalBookingsCancel { get; set; }
		public RevenueDetail? DetailsRevenue { get; set; }
		public IList<SportCount>? CountBookings { get; set; }
	}

	public class RevenueDetail
	{
		public IList<HourlyRevenue>? HourlyDetails { get; set; }
		public IList<DayOfWeekRevenue>? DayOfWeekDetails { get; set; }
		public IList<DailyRevenue>? DailyDetails { get; set; }
		public IList<MonthlyRevenue>? MonthlyDetails { get; set; }
	}
	public class HourlyRevenue
	{
		public TimeSpan Hour { get; set; }
		public decimal Amount { get; set; }
	}
	public class DayOfWeekRevenue
	{
		public DateOnly Day { get; set; }
		public decimal Amount { get; set; }
	}

	public class DailyRevenue
	{
		public DateOnly Day { get; set; }
		public decimal Amount { get; set; }
	}

	public class MonthlyRevenue
	{
		public int Month { get; set; }
		public decimal Amount { get; set; }
	}

	public class SportCount
	{
		public required string SportName { get; set; }
		public int Count { get; set; }
	}
}
