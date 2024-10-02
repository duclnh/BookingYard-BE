using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetAllBookingFacility
{
	public record GetAllBookingFacilityQuery(RequestParams requestParams, Guid facilityId, CancellationToken cancellation) : IRequest<PagingResult<BookingDetailDto>>
	{
	}
}
