using System;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Commands.CancelBooking;

public class CancelBookingCommand : IRequest<string>
{
    public Guid BookingID { get; set; }
}
