using System;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Commands.CheckInBooking;

public class CheckInBookingCommand : IRequest<string>
{
    public Guid BookingID { get; set; }
    public Guid FacilityID { get; set; }
}
