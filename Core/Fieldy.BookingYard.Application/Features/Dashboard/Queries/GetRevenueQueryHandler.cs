﻿using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
			var startOfWeek = today.Date.AddDays(-(int)today.DayOfWeek + 1);
			var endOfWeek = startOfWeek.AddDays(7);
			Expression<Func<Domain.Entities.Booking, bool>> timeBasedExpression = request.TypeTimeBased switch
			{
				/*TypeTimeBased.date*/
				"date" => x => x.BookingDate.Date == today && x.PaymentStatus,
				/*TypeTimeBased.week*/
				"week" => x => x.BookingDate >= startOfWeek && x.PaymentStatus &&
					  x.BookingDate < endOfWeek,
				/*TypeTimeBased.month*/
				"month" => x => x.BookingDate.Month == today.Month && x.PaymentStatus &&
					 x.BookingDate.Year == today.Year,
				/*TypeTimeBased.year*/
				"year" => x => x.BookingDate.Year == today.Year && x.PaymentStatus,
				"from" => x => x.BookingDate.Date > request.FromDate.GetValueOrDefault().Date && x.PaymentStatus,
				"to" => x => x.BookingDate.Date < request.FromDate.GetValueOrDefault().Date && x.PaymentStatus,
				"both" => x => x.BookingDate.Date > request.FromDate.GetValueOrDefault().Date
				   && x.BookingDate.Date < request.ToDate.GetValueOrDefault().Date
				   && x.PaymentStatus,
				_ => x => false
			};
			var bookings = await _bookingRepository.FindAll(expression: timeBasedExpression,
															includes: new Expression<Func<Domain.Entities.Booking, object>>[] {
																x => x.Court,
																x => x.Court.Sport,
															});
			var revenueDetail = new RevenueDetail();
			switch (request.TypeTimeBased)
			{
				case /*TypeTimeBased.date*/ "date":
					var revenueByHour = _bookingRepository.GetRevenueByHour(Guid.Empty);
					revenueDetail.HourlyDetails = revenueByHour?.Select(r => new HourlyRevenue
					{
						Hour = r.Hour,
						Amount = r.TotalRevenue
					}).ToList();
					break;
				case /*TypeTimeBased.week*/ "week":
					var revenueByWeek = _bookingRepository.GetRevenueByWeek(Guid.Empty);
					revenueDetail.DayOfWeekDetails = revenueByWeek?.Select(r => new DayOfWeekRevenue
					{
						Day = r.Date,
						Amount = r.TotalRevenue
					}).ToList();
					break;
				case /*TypeTimeBased.month*/ "month":
					var revenueByDay = _bookingRepository.GetRevenueByDay(Guid.Empty, null, null);
					revenueDetail.DailyDetails = revenueByDay?.Select(r => new DailyRevenue
					{
						Day = r.Date,
						Amount = r.TotalRevenue
					}).ToList();
					break;
				case /*TypeTimeBased.month*/ "from":
					var revenueByFrom = _bookingRepository.GetRevenueByDay(Guid.Empty, request.FromDate, null);
					revenueDetail.DailyDetails = revenueByFrom?.Select(r => new DailyRevenue
					{
						Day = r.Date,
						Amount = r.TotalRevenue
					}).ToList();
					break;
				case /*TypeTimeBased.month*/ "to":
					var revenueByTo = _bookingRepository.GetRevenueByDay(Guid.Empty, null, request.ToDate);
					revenueDetail.DailyDetails = revenueByTo?.Select(r => new DailyRevenue
					{
						Day = r.Date,
						Amount = r.TotalRevenue
					}).ToList();
					break;
				case /*TypeTimeBased.month*/ "both":
					var revenueByTBoth = _bookingRepository.GetRevenueByDay(Guid.Empty, request.FromDate, request.ToDate);
					revenueDetail.DailyDetails = revenueByTBoth?.Select(r => new DailyRevenue
					{
						Day = r.Date,
						Amount = r.TotalRevenue
					}).ToList();
					break;
				case /*TypeTimeBased.year*/ "year":
					var revenueByMonth = _bookingRepository.GetRevenueByMonth(Guid.Empty);
					revenueDetail.MonthlyDetails = revenueByMonth?.Select(r => new MonthlyRevenue
					{
						Month = r.Month,
						Amount = r.TotalRevenue
					}).ToList();
					break;
			}
			var sportsCounts = bookings
				.Where(b => b.IsDeleted == false)
				.GroupBy(b => b.Court.Sport.SportName)
				.Select(g => new SportCount
				{
					SportName = g.Key,
					Count = g.Count()
				})
				.ToList();

			var totalPrice = bookings.Sum(booking => !booking.IsDeleted ? booking.TotalPrice - booking.OwnerPrice : 0);
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
