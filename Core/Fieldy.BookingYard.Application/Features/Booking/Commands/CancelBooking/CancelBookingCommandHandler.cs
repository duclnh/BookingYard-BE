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

    public CancelBookingCommandHandler(IJWTService jWTService, IUserRepository userRepository, IBookingRepository bookingRepository)
    {
        _jWTService = jWTService;
        _userRepository = userRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<string> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Find(x => x.Id == _jWTService.UserID, cancellationToken);
        if (user == null || user.Role != Role.CourtOwner || user.Role != Role.Customer)
            throw new BadRequestException($"You don't have permission");

        var booking = await _bookingRepository.Find(x => x.Id == request.BookingID, cancellationToken);
		if(booking == null)
            throw new NotFoundException(nameof(booking), request.BookingID);

        if (booking.Status == true)
            throw new BadRequestException("Booking is already used");

		var historyPoint = await _historyPointRepository.Find(x => x.UserID == _jWTService.UserID, cancellationToken);
		if (historyPoint != null)
		{
			historyPoint.Point += booking.TotalPrice;
			_historyPointRepository.Update(historyPoint);
		}
		else
        {
            var historyPointEntity = new Domain.Entities.HistoryPoint
            {
                Id = 1,
				UserID = booking.UserID,
				Point = booking.TotalPrice,
                CreatedAt = DateTime.Now,
                Content = "Cancel booking " + booking.PaymentCode,
			};
            await _historyPointRepository.AddAsync(historyPointEntity);
		}
		var addingPoint = await _historyPointRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        if (addingPoint < 0)
            throw new BadRequestException("Cancel booking fail");

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
