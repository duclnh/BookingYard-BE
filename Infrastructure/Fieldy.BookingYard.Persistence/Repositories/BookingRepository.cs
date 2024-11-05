using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class BookingRepository : RepositoryBase<Booking, Guid>, IBookingRepository
	{
		public BookingRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{
		}
		public List<(TimeSpan Hour, decimal TotalRevenue)> GetRevenueByHour(Guid facilityId)
		{
			var date = DateTime.Now;

			var revenues = new List<(TimeSpan Hour, decimal TotalRevenue)>();

			if (facilityId == Guid.Empty)
			{
				revenues = _dbContext.Set<Booking>()
				.Where(b => b.BookingDate.Date == date.Date && b.IsDeleted == false)
				.GroupBy(b => b.StartTime)
				.Select(g => new
				{
					Hour = g.Key,
					TotalRevenue = g.Sum(b => b.TotalPrice - b.OwnerPrice)
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
			}
			else
			{
				revenues = _dbContext.Set<Booking>()
				.Include(b => b.Court)
				.Where(b => b.BookingDate.Date == date.Date && b.IsDeleted == false && b.Court.FacilityID == facilityId)
				.GroupBy(b => b.StartTime)
				.Select(g => new
				{
					Hour = g.Key,
					TotalRevenue = g.Sum(b => b.OwnerPrice)
				})
				.AsEnumerable()
				.Select(x => (x.Hour, x.TotalRevenue))
				.ToList();

				var facility = _dbContext.Set<Facility>().FirstOrDefault(f => f.Id == facilityId);

				/*var allHours = Enumerable.Range(facility.StartTime.Hours, facility.EndTime.Hours)
								 .Select(hour => new TimeSpan(hour, 0, 0))
								 .ToHashSet();*/
				var existingHours = new HashSet<TimeSpan>(revenues.Select(r => r.Hour));
				HashSet<TimeSpan> timeSet = new HashSet<TimeSpan>();
				for (TimeSpan currentTime = facility.StartTime; currentTime <= facility.EndTime; currentTime = currentTime.Add(new TimeSpan(1, 0, 0)))
				{
					timeSet.Add(currentTime);
				}
				foreach (var missingHour in timeSet.Except(existingHours))
				{
					revenues.Add((missingHour, 0.00m));
				}
			}

			return revenues.OrderBy(r => r.Hour).ToList();
		}

		public List<(string Date, decimal TotalRevenue)> GetRevenueByWeek(Guid facilityId)
		{
			var date = DateTime.Now;
			var startOfWeek = date.Date.AddDays(-(int)date.DayOfWeek + 1);
			var endOfWeek = startOfWeek.AddDays(7);

			var revenues = new List<(DateOnly Date, decimal TotalRevenue)>();
			var all = _dbContext.Bookings.ToList();
			if (facilityId == Guid.Empty)
			{
				revenues = _dbContext.Set<Booking>()
							.Where(b => b.BookingDate >= startOfWeek && b.IsDeleted == false)
							.GroupBy(b => b.BookingDate.Date)
							.Select(g => new
							{
								Date = DateOnly.FromDateTime(g.Key),
								TotalRevenue = g.Sum(b => b.TotalPrice - b.OwnerPrice)
							})
							.AsEnumerable()
							.Select(x => (x.Date, x.TotalRevenue))
							.ToList();
			}
			else
			{
				revenues = _dbContext.Set<Booking>()
							.Where(b => b.BookingDate >= startOfWeek && b.IsDeleted == false && b.Court.FacilityID == facilityId)
							.GroupBy(b => b.BookingDate.Date)
							.Select(g => new
							{
								Date = DateOnly.FromDateTime(g.Key),
								TotalRevenue = g.Sum(b => b.OwnerPrice)
							})
							.AsEnumerable()
							.Select(x => (x.Date, x.TotalRevenue))
							.ToList();
			}
			
			var allDates = Enumerable.Range(0, (endOfWeek - startOfWeek).Days)
							 .Select(offset => startOfWeek.AddDays(offset))
							 .Select(date => DateOnly.FromDateTime(date))
							 .ToHashSet();
			var existingDates = new HashSet<DateOnly>(revenues.Select(r => r.Date));
			foreach (var missingDate in allDates.Except(existingDates))
			{
				revenues.Add((missingDate, 0.00m));
			}
			revenues = revenues.OrderBy(r => r.Date).ToList();

			List<(string Date, decimal TotalRevenue)> convertedList = revenues
			.Select(x => (x.Date.ToString("dddd"), x.TotalRevenue))
			.ToList();
			return convertedList;
		}

		public List<(DateOnly Date, decimal TotalRevenue)> GetRevenueByDay(Guid facilityId)
		{
			var date = DateTime.Now;
			var startOfMonth = new DateTime(date.Year, date.Month, 1);
			var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

			// Use an anonymous type to hold the intermediate results
			var revenues = new List<(DateOnly Date, decimal TotalRevenue)>();

			if (facilityId == Guid.Empty)
			{
				revenues = _dbContext.Set<Booking>()
					.Where(b => b.BookingDate >= startOfMonth && !b.IsDeleted)
					.GroupBy(b => b.BookingDate.Day)
					.Select(g => new
					{
						Date = new DateOnly(date.Year, date.Month, g.Key),
						TotalRevenue = g.Sum(b => b.TotalPrice - b.OwnerPrice)
					})
					.AsEnumerable()
					.Select(x => (x.Date, x.TotalRevenue))
					.ToList();
			}
			else
			{
				revenues = _dbContext.Set<Booking>()
					.Include(b => b.Court)
					.Where(b => b.BookingDate >= startOfMonth && !b.IsDeleted && b.Court.FacilityID == facilityId)
					.GroupBy(b => b.BookingDate.Day)
					.Select(g => new
					{
						Date = new DateOnly(date.Year, date.Month, g.Key),
						TotalRevenue = g.Sum(b => b.OwnerPrice)
					})
					.AsEnumerable()
					.Select(x => (x.Date, x.TotalRevenue))
					.ToList();
			}

			int numberOfDays = (endOfMonth - startOfMonth).Days + 1;
			var allDates = Enumerable.Range(1, numberOfDays)
									 .Select(day => new DateOnly(date.Year, date.Month, day))
									 .ToHashSet();

			var existingDates = new HashSet<DateOnly>(revenues.Select(r => r.Date));
			var missingDates = allDates.Except(existingDates);

			foreach (var missingDate in missingDates)
			{
				revenues.Add((missingDate, 0.00m));
			}

			return revenues.OrderBy(x => x.Date).ToList();
		}



		public List<(int Month, decimal TotalRevenue)> GetRevenueByMonth(Guid facilityId)
		{
			var date = DateTime.Now;
			var startOfYear = new DateTime(date.Year, 1, 1);
			var endOfYear = new DateTime(date.Year, 12, 31);

			var revenues = new List<dynamic>();

			if (facilityId == Guid.Empty)
			{
				revenues = _dbContext.Set<Booking>()
					.Where(b => b.BookingDate >= startOfYear && b.BookingDate <= endOfYear && b.IsDeleted == false)
					.GroupBy(b => b.BookingDate.Month)
					.Select(g => new
					{
						Month = g.Key,
						TotalRevenue = g.Sum(b => b.OwnerPrice)
					})
					.ToList<dynamic>();
			}
			else
			{
				revenues = _dbContext.Set<Booking>()
					.Include(b => b.Court)
					.Where(b => b.BookingDate >= startOfYear && b.BookingDate <= endOfYear && b.IsDeleted == false && b.Court.FacilityID == facilityId)
					.GroupBy(b => b.BookingDate.Month)
					.Select(g => new
					{
						Month = g.Key,
						TotalRevenue = g.Sum(b => b.OwnerPrice)
					})
					.ToList<dynamic>();
			}

			for (int i = 1; i <= endOfYear.Month; i++)
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
