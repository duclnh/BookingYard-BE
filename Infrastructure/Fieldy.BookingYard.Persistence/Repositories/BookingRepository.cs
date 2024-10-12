using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class BookingRepository : RepositoryBase<Booking, Guid>, IBookingRepository
	{
		public BookingRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{
		}
		public List<(TimeSpan Hour, decimal TotalRevenue)> GetRevenueByHour()
		{
			var date = DateTime.Now;

			var revenues = _dbContext.Set<Booking>()
				.Where(b => b.BookingDate.Date == date.Date)
				.GroupBy(b => b.StartTime)
				.Select(g => new
				{
					Hour = g.Key,
					TotalRevenue = g.Sum(b => b.TotalPrice)
				})
				.AsEnumerable()
				.Select(x => (x.Hour, x.TotalRevenue))
				.ToList();

			return revenues;
		}

		public List<(DateOnly Date, decimal TotalRevenue)> GetRevenueByWeek()
		{
			var date = DateTime.Now;
			var startOfWeek = date.Date.AddDays(-(int)date.DayOfWeek);
			var endOfWeek = startOfWeek.AddDays(7);

			var revenues = _dbContext.Set<Booking>()
				.Where(b => b.BookingDate >= startOfWeek && b.BookingDate < endOfWeek)
				.GroupBy(b => b.BookingDate.Date)
				.Select(g => new
				{
					Date = DateOnly.FromDateTime(g.Key),
					TotalRevenue = g.Sum(b => b.TotalPrice)
				})
				.AsEnumerable()
				.Select(x => (x.Date, x.TotalRevenue))
				.ToList();

			return revenues;
		}


		public List<(DateOnly Date, decimal TotalRevenue)> GetRevenueByDay()
		{
			var date = DateTime.Now;
			var startOfMonth = new DateTime(date.Year, date.Month, 1);
			var endOfMonth = startOfMonth.AddMonths(1);

			var revenues = _dbContext.Set<Booking>()
				.Where(b => b.BookingDate >= startOfMonth && b.BookingDate < endOfMonth)
				.GroupBy(b => b.BookingDate.Day)
				.Select(g => new
				{
					Date = new DateOnly(date.Year, date.Month, g.Key),
					TotalRevenue = g.Sum(b => b.TotalPrice)
				})
				.AsEnumerable()
				.Select(x => (x.Date, x.TotalRevenue))
				.ToList();

			return revenues;
		}

		public List<(int Month, decimal TotalRevenue)> GetRevenueByMonth()
		{
			var date = DateTime.Now;
			var startOfYear = new DateTime(date.Year, 1, 1);
			var endOfYear = startOfYear.AddYears(1);

			var revenues = _dbContext.Set<Booking>()
				.Where(b => b.BookingDate >= startOfYear && b.BookingDate < endOfYear)
				.GroupBy(b => b.BookingDate.Month)
				.Select(g => new
				{
					Month = g.Key,
					TotalRevenue = g.Sum(b => b.TotalPrice)
				})
				.ToList();

			return revenues.Select(x => (x.Month, x.TotalRevenue)).ToList();
		}
	}
}
