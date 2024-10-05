using System;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Commands.CancelBooking;

public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, string>
{
    private readonly IJWTService _jWTService;
    private readonly IUserRepository _userRepository;
    private readonly IBookingRepository _bookingRepository;

    public CancelBookingCommandHandler(IJWTService jWTService, IUserRepository userRepository, IBookingRepository bookingRepository)
    {
        _jWTService = jWTService;
        _userRepository = userRepository;
        _bookingRepository = bookingRepository;
    }

    public Task<string> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
