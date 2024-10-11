using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;
using System.Linq.Expressions;

namespace Fieldy.BookingYard.Application.Features.Dashboard.Queries
{
	public class GetRevenueQueryHandler : IRequestHandler<GetRevenueQuery, DashboardHome>
	{
		private readonly IBookingRepository _bookingRepository;
		public GetRevenueQueryHandler(IBookingRepository bookingRepository)
		{
			_bookingRepository = bookingRepository;
		}
		public async Task<DashboardHome> Handle(GetRevenueQuery request, CancellationToken cancellationToken)
		{
			var today = DateTime.Today;
			Expression<Func<Domain.Entities.Booking, bool>> timeBasedExpression = request.typeTimeBased switch
			{
				TypeTimeBased.date => x => x.BookingDate.Date == today && x.IsDeleted == (request.typeBooking == 0 ? true : false),
				TypeTimeBased.week => x => x.BookingDate >= today.AddDays(-(int)today.DayOfWeek) &&
											 x.BookingDate < today.AddDays(7 - (int)today.DayOfWeek) && x.IsDeleted == (request.typeBooking == 0 ? true : false),
				TypeTimeBased.month => x => x.BookingDate.Month == today.Month &&
											 x.BookingDate.Year == today.Year && x.IsDeleted == (request.typeBooking == 0 ? true : false),
				TypeTimeBased.year => x => x.BookingDate.Year == today.Year && x.IsDeleted == (request.typeBooking == 0 ? true : false),
				_ => x => true
			};
			var bookings = await _bookingRepository.FindAll(expression: timeBasedExpression,
															includes: new Expression<Func<Domain.Entities.Booking, object>>[] {
																x => x.Court,
																x => x.Court.Sport,
															});
			var revenueDetail = new RevenueDetail();
			switch (request.typeTimeBased)
			{
				case TypeTimeBased.date:
					var revenueByHour = _bookingRepository.GetRevenueByHour();
					revenueDetail.HourlyDetails = revenueByHour?.Select(r => new HourlyRevenue
					{
						Hour = r.Hour,
						Amount = r.TotalRevenue
					}).ToList();
					break;
				case TypeTimeBased.week:
					var revenueByWeek = _bookingRepository.GetRevenueByWeek();
					revenueDetail.DayOfWeekDetails = revenueByWeek?.Select(r => new DayOfWeekRevenue
					{
						Day = r.Date,
						Amount = r.TotalRevenue
					}).ToList();
					break;
				case TypeTimeBased.month:
					var revenueByDay = _bookingRepository.GetRevenueByDay();
					revenueDetail.DailyDetails = revenueByDay?.Select(r => new DailyRevenue
					{
						Day = r.Date,
						Amount = r.TotalRevenue
					}).ToList();
					break;
				case TypeTimeBased.year:
					var revenueByMonth = _bookingRepository.GetRevenueByMonth();
					revenueDetail.MonthlyDetails = revenueByMonth?.Select(r => new MonthlyRevenue
					{
						Month = r.Month,
						Amount = r.TotalRevenue
					}).ToList();
					break;
			}
			var sportsCounts = bookings
				.GroupBy(b => b.Court.Sport.SportName)
				.Select(g => new SportCount
				{
					SportName = g.Key,
					Count = g.Count()
				})
				.ToList();

			var totalPrice = bookings.Sum(booking => booking.TotalPrice);
			return new DashboardHome
			{
				Revenue = totalPrice,
				TotalBookings = bookings.Count,
				TotalBookingsCancel = bookings.Count(x => x.IsDeleted == true),
				DetailsRevenue = revenueDetail,
				CountBookings = sportsCounts
			};
		}
	}
}
