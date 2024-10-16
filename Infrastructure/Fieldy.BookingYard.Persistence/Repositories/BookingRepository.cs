using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

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
				.Where(b => b.BookingDate.Date == date.Date && b.IsDeleted == false)
				.GroupBy(b => b.StartTime)
				.Select(g => new
				{
					Hour = g.Key,
					TotalRevenue = g.Sum(b => b.TotalPrice)
				})
				.AsEnumerable()
				.Select(x => (x.Hour, x.TotalRevenue))
				.ToList(); 
			
			var allHours = Enumerable.Range(0, 24)
							 .Select(hour => new TimeSpan(hour, 0, 0))
							 .ToHashSet();
			var existingHours = new HashSet<TimeSpan>(revenues.Select(r => r.Hour));
			foreach (var missingHour in allHours.Except(existingHours))
			{
				revenues.Add((missingHour, 0.00m));
			}

			return revenues.OrderBy(r => r.Hour).ToList();
		}

		public List<(DateOnly Date, decimal TotalRevenue)> GetRevenueByWeek()
		{
			var date = DateTime.Now;
			var startOfWeek = date.Date.AddDays(-(int)date.DayOfWeek);
			var endOfWeek = startOfWeek.AddDays(7);

			var revenues = _dbContext.Set<Booking>()
				.Where(b => b.BookingDate >= startOfWeek && b.BookingDate < endOfWeek && b.IsDeleted == false)
				.GroupBy(b => b.BookingDate.Date)
				.Select(g => new
				{
					Date = DateOnly.FromDateTime(g.Key),
					TotalRevenue = g.Sum(b => b.TotalPrice)
				})
				.AsEnumerable()
				.Select(x => (x.Date, x.TotalRevenue))
				.ToList(); 
			
			var allDates = Enumerable.Range(0, 7)
							 .Select(offset => startOfWeek.AddDays(offset))
							 .Select(date => DateOnly.FromDateTime(date))
							 .ToHashSet();
			var existingDates = new HashSet<DateOnly>(revenues.Select(r => r.Date));
			foreach (var missingDate in allDates.Except(existingDates))
			{
				revenues.Add((missingDate, 0.00m));
			}

			return revenues.OrderBy(r => r.Date).ToList();
		}


		public List<(DateOnly Date, decimal TotalRevenue)> GetRevenueByDay()
		{
			var date = DateTime.Now;
			var startOfMonth = new DateTime(date.Year, date.Month, 1);
			var endOfMonth = startOfMonth.AddMonths(1);

			var revenues = _dbContext.Set<Booking>()
				.Where(b => b.BookingDate >= startOfMonth && b.BookingDate < endOfMonth && b.IsDeleted == false)
				.GroupBy(b => b.BookingDate.Day)
				.Select(g => new
				{
					Date = new DateOnly(date.Year, date.Month, g.Key),
					TotalRevenue = g.Sum(b => b.TotalPrice)
				})
				.AsEnumerable()
				.Select(x => (x.Date, x.TotalRevenue))
			.ToList();

			
			int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
			var allDates = Enumerable.Range(1, daysInMonth)
									 .Select(day => new DateOnly(date.Year, date.Month, day))
									 .ToHashSet();
			var existingDates = new HashSet<DateOnly>(revenues.Select(r => r.Date));
			var missingDates = allDates.Except(existingDates);
			foreach (var missingDate in missingDates)
			{
				revenues.Add((missingDate, 0.00m));
			}

			return revenues.OrderBy(r => r.Date).ToList();
		}

		public List<(int Month, decimal TotalRevenue)> GetRevenueByMonth()
		{
			var date = DateTime.Now;
			var startOfYear = new DateTime(date.Year, 1, 1);
			var endOfYear = startOfYear.AddYears(1);

			var revenues = _dbContext.Set<Booking>()
				.Where(b => b.BookingDate >= startOfYear && b.BookingDate < endOfYear && b.IsDeleted == false)
				.GroupBy(b => b.BookingDate.Month)
				.Select(g => new
				{
					Month = g.Key,
					TotalRevenue = g.Sum(b => b.TotalPrice)
				})
				.ToList();

			int monthsInYear = 12;
			for (int i = 1; i <= monthsInYear; i++)
			{
				if (!revenues.Any(r => r.Month == i))
				{
					revenues.Add(new { Month = i, TotalRevenue = 0.00m });
				}
			}

			return revenues.Select(r => ((int)r.Month, (decimal)r.TotalRevenue)).OrderBy(x => x.Item1).ToList();
		}
	}
}
