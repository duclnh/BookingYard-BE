using System;
using System.Security.Cryptography.X509Certificates;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Commands.CheckInBooking;

public class CheckInBookingCommandHandler : IRequestHandler<CheckInBookingCommand, string>
{
    private readonly IBookingRepository _bookingRepository;

    public CheckInBookingCommandHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<string> Handle(CheckInBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.Find(x => x.Id == request.BookingID
                                                    && x.Court.FacilityID == request.FacilityID,
                                                    cancellationToken,
                                                    x => x.Court);

        if (booking == null)
            throw new NotFoundException(nameof(booking), request.BookingID);

        booking.IsCheckin = true;
        booking.PaymentStatus = true;
        
        _bookingRepository.Update(booking);

        var result = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        if (result < 0)
            throw new BadRequestException("Check in Fail");

        return "Check In Successfully";
    }
}
