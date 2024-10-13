using System;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetQrCode;

public class GetQrCodeQueryHandler : IRequestHandler<GetQrCodeQuery, string>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUtilityService _utilityService;
    private readonly IJWTService _jWTService;

    public GetQrCodeQueryHandler(IBookingRepository bookingRepository, IUtilityService utilityService, IJWTService jWTService)
    {
        _bookingRepository = bookingRepository;
        _utilityService = utilityService;
        _jWTService = jWTService;
    }

    public async Task<string> Handle(GetQrCodeQuery request, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.FindByIdAsync(request.bookingID, cancellationToken);

        if (booking == null)
            throw new NotFoundException(nameof(booking), request.bookingID);

        if (booking.UserID != _jWTService.UserID)
            throw new BadRequestException("You don't have permission");

        return _utilityService.CreateQrCode(booking.PaymentCode, booking.FullName, booking.Email, booking.Phone);
    }
}
