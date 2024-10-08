using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetAllBookingCustomer
{
	public record GetAllBookingCustomerQuery(RequestParams requestParams, Guid userId, string type, CancellationToken cancellation) : IRequest<PagingResult<CustomerBooking>>
	{
	}
}
