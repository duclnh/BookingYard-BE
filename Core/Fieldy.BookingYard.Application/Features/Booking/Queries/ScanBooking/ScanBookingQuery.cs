using System;
using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.ScanBooking;

public record class ScanBookingQuery(RequestParams RequestParams, Guid FacilityID, string? Email, string? Phone, string? Code) : IRequest<PagingResult<BookingDetailDto>>
{

}
