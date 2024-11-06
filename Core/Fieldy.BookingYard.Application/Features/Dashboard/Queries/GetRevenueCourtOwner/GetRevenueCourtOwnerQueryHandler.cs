using Fieldy.BookingYard.Application.Features.Dashboard.Queries.DTO;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Fieldy.BookingYard.Application.Features.Dashboard.Queries.GetRevenueCourtOwner
{
	public class GetRevenueCourtOwnerQueryHandler : IRequestHandler<GetRevenueCourtOwnerQuery, DashboardCourtOwner>
	{
		private readonly IBookingRepository _bookingRepository;
		public GetRevenueCourtOwnerQueryHandler(IBookingRepository bookingRepository)
		{
			_bookingRepository = bookingRepository;
		}
		public async Task<DashboardCourtOwner> Handle(GetRevenueCourtOwnerQuery request, CancellationToken cancellationToken)
		{
			var today = DateTime.Today;
			Expression<Func<Domain.Entities.Booking, bool>> timeBasedExpression = request.TypeTimeBased switch
			{
				"date" => x => x.BookingDate.Date == today && x.Court.FacilityID == request.FacilityId && x.PaymentStatus,

				"week" => x => x.BookingDate >= today.AddDays(-(int)today.DayOfWeek) &&
					  x.BookingDate < today.AddDays(7 - (int)today.DayOfWeek) &&
					  x.Court.FacilityID == request.FacilityId && x.PaymentStatus,

				"month" => x => x.BookingDate.Month == today.Month &&
					  x.BookingDate.Year == today.Year &&
					  x.Court.FacilityID == request.FacilityId && x.PaymentStatus,

				"year" => x => x.BookingDate.Year == today.Year &&
					  x.Court.FacilityID == request.FacilityId && x.PaymentStatus,
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
																x => x.Court.Facility
															});

			var revenueDetail = new RevenueDetail();
			switch (request.TypeTimeBased)
			{
				case "date":
					var revenueByHour = _bookingRepository.GetRevenueByHour(request.FacilityId);
					revenueDetail.HourlyDetails = revenueByHour?.Select(r => new HourlyRevenue
					{
						Hour = r.Hour,
						Amount = r.TotalRevenue
					}).ToList();
					break;
				case "week":
					var revenueByWeek = _bookingRepository.GetRevenueByWeek(request.FacilityId);
					revenueDetail.DayOfWeekDetails = revenueByWeek?.Select(r => new DayOfWeekRevenue
					{
						Day = r.Date,
						Amount = r.TotalRevenue
					}).ToList();
					break;
				case "month":
					var revenueByDay = _bookingRepository.GetRevenueByDay(request.FacilityId, request.FromDate, request.ToDate);
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
				case "year":
					var revenueByMonth = _bookingRepository.GetRevenueByMonth(request.FacilityId);
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

			var totalPrice = bookings.Sum(booking => !booking.IsDeleted ? booking.OwnerPrice : 0);
			return new DashboardCourtOwner
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
