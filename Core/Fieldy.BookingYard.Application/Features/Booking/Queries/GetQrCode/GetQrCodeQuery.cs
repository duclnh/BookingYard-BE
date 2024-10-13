using System;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetQrCode;

public record class GetQrCodeQuery(Guid bookingID) : IRequest<string>
{

}
