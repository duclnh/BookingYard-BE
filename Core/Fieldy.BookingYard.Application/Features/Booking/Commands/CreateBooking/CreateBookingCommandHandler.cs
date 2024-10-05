using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Commands.CreateBooking
{
	public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IBookingRepository _bookingRepository;
		private readonly IVnpayService _vnpayService;

		public CreateBookingCommandHandler(IMapper mapper, IBookingRepository bookingRepository, IVnpayService vnpayService)
		{
			_mapper = mapper;
			_bookingRepository = bookingRepository;
			_vnpayService = vnpayService;
		}

		public async Task<string> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateBookingCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register booking", validationResult);

			var booking = _mapper.Map<Domain.Entities.Booking>(request);
			booking.CreatedAt = DateTime.Now;
			booking.CreatedBy = request.UserID;
			booking.ModifiedAt = DateTime.Now;
			booking.ModifiedBy = request.UserID;

			DateTime requestDate = DateTime.Now;
			booking.PaymentCode = "FIELDY" + requestDate.ToString("yyyyMMddHHmmss");

			if (booking == null)
				throw new BadRequestException("Error create booking!");

			await _bookingRepository.AddAsync(booking);

			var result = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result <= 0)
				throw new BadRequestException("Create new booking fail!");

			return _vnpayService.CreateRequestUrl(booking.TotalPrice, booking.PaymentCode, requestDate);
		}
	}
}
