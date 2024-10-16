using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories
{
	public interface IBookingRepository : IRepositoryBase<Booking, Guid>
	{
		List<(TimeSpan Hour, decimal TotalRevenue)> GetRevenueByHour();
		List<(string Date, decimal TotalRevenue)> GetRevenueByWeek();
		List<(DateOnly Date, decimal TotalRevenue)> GetRevenueByDay();
		List<(int Month, decimal TotalRevenue)> GetRevenueByMonth();
	}
}
