using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Commands.CancelBooking;

public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, string>
{
    private readonly IJWTService _jWTService;
    private readonly IUserRepository _userRepository;
    private readonly IHistoryPointRepository _historyPointRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly ICourtRepository _courtRepository;


    public CancelBookingCommandHandler(IJWTService jWTService,
                                        IUserRepository userRepository,
                                        IBookingRepository bookingRepository,
                                        ICourtRepository courtRepository,
                                        IHistoryPointRepository historyPointRepository)
    {
        _jWTService = jWTService;
        _userRepository = userRepository;
        _historyPointRepository = historyPointRepository;
        _bookingRepository = bookingRepository;
        _courtRepository = courtRepository;
    }

    public async Task<string> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Find(x => x.Id == _jWTService.UserID, cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(user), _jWTService.UserID);

        var booking = await _bookingRepository.Find(x => x.Id == request.BookingID,
                                                    cancellationToken,
                                                    x => x.User);

        if (booking == null)
            throw new NotFoundException(nameof(booking), request.BookingID);

        if (user.Role == Role.Customer && user.Id != booking.UserID)
            throw new BadRequestException($"You don't have permission");

        if (user.Role == Role.StaffCourt || user.Role == Role.CourtOwner)
        {
            var court = await _courtRepository.Find(x => x.Id == booking.CourtID, cancellationToken, x => x.Facility);
            if (court != null && court?.Facility?.UserID != user.Id)
                throw new BadRequestException($"You don't have permission");
        }

        if (booking.IsCheckin == true)
            throw new BadRequestException("Booking is already used");

        if (booking.User != null)
        {
            booking.User.Point += (int)booking.TotalPrice;
            _userRepository.Update(booking.User);
        }
        var historyPointEntity = new Domain.Entities.HistoryPoint
        {
            Id = 0,
            UserID = booking.UserID,
            Point = (int)booking.TotalPrice,
            CreatedAt = DateTime.Now,
            Content = "Huỷ đặt lịch " + booking.PaymentCode,
        };

        await _historyPointRepository.AddAsync(historyPointEntity);

        booking.Reason = request.Reason;
        booking.IsDeleted = true;
        booking.ModifiedAt = DateTime.Now;
        booking.ModifiedBy = _jWTService.UserID;

        _bookingRepository.Update(booking);
        var result = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        if (result < 0)
            throw new BadRequestException("Cancel booking fail");
        return "Cancel booking success";
    }
}
