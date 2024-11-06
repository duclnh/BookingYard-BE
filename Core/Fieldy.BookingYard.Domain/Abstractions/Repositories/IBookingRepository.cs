using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories
{
	public interface IBookingRepository : IRepositoryBase<Booking, Guid>
	{
		List<(TimeSpan Hour, decimal TotalRevenue)> GetRevenueByHour(Guid facilityId);
		List<(string Date, decimal TotalRevenue)> GetRevenueByWeek(Guid facilityId);
		List<(DateOnly Date, decimal TotalRevenue)> GetRevenueByDay(Guid facilityId, DateTime? fromDate, DateTime? toDate);
		List<(int Month, decimal TotalRevenue)> GetRevenueByMonth(Guid facilityId);
		//List<(TimeSpan Hour, decimal TotalRevenue)> GetRevenueByHour(Guid facilityId);
	}
}
