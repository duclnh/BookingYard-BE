using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Commands.CreateBooking
{
	public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
	{
		private readonly IMapper _mapper;
		private readonly IBookingRepository _bookingRepository;
		public CreateBookingCommandHandler(IMapper mapper, IBookingRepository bookingRepository)
		{
			_mapper = mapper;
			_bookingRepository = bookingRepository;
		}
		public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
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
			booking.Id = Guid.NewGuid();
			booking.IsDeleted = false;
			booking.Status = false;
			booking.PaymentStatus = false;


			if (booking == null)
				throw new BadRequestException("Error create booking!");

			var result = await _bookingRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Create new booking fail!");

			return booking.Id;
		}
	}
}
