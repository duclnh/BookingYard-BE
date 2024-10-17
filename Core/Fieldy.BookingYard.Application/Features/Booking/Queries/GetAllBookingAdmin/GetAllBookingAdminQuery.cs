using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetAllBookingFacility
{
	public record GetAllBookingAdminQuery(RequestParams requestParams, string? status) : IRequest<PagingResult<BookingDetailDto>>
	{
	}
}
