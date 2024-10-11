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
				CountBookings = sportsCounts
			};
		}
	}
}
