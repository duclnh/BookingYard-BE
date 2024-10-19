using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback
{
	public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IFeedbackRepository _feedbackRepository;
		private readonly IBookingRepository _bookingRepository;
		private readonly IUtilityService _utilityService;
		private readonly IUserRepository _userRepository;
		private readonly IHistoryPointRepository _historyPointRepository;

		public CreateFeedbackCommandHandler(IMapper mapper, IFeedbackRepository feedbackRepository, IBookingRepository bookingRepository, IUtilityService utilityService, IUserRepository userRepository, IHistoryPointRepository historyPointRepository)
		{
			_mapper = mapper;
			_feedbackRepository = feedbackRepository;
			_bookingRepository = bookingRepository;
			_utilityService = utilityService;
			_userRepository = userRepository;
			_historyPointRepository = historyPointRepository;
		}

		public async Task<string> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateFeedbackCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register Feedback", validationResult);

			var booking = await _bookingRepository.FindByIdAsync(request.BookingID, cancellationToken);
			if (booking == null)
				throw new NotFoundException(nameof(booking), request.BookingID);

			if(request.UserID != booking.UserID)
			 	throw new UnauthorizedAccessException("User does not have permission to access this booking");

			if (booking.IsDeleted)
				throw new BadRequestException("Booking has not been deleted");		

			if (!booking.IsCheckin)
				throw new BadRequestException("Check-in has not been completed");

			if (booking.IsFeedback)
				throw new BadRequestException("Feedback has already been provided");


			var user = await _userRepository.FindByIdAsync(request.UserID, cancellationToken);
			if (user == null)
				throw new NotFoundException(nameof(user), request.UserID);

			var feedback = _mapper.Map<Domain.Entities.FeedBack>(request);
			feedback.IsShow = true;
			feedback.CreatedAt = DateTime.Now;
			feedback.TypeFeedback = TypeFeedback.Customer;
			booking.IsFeedback = true;
			_bookingRepository.Update(booking);

			if (request.FeedbackImages?.Any() == true)
			{
				feedback.Images = request.FeedbackImages
										 .Select(async file => new Image
										 {
											 Id = 0,
											 ImageLink = await _utilityService.AddFile(file, "facility"),
										 })
										 .Select(t => t.Result)
										 .ToList();
			}
			int point = (int)Math.Ceiling(booking.TotalPrice * ((decimal)3 / 100));
			user.Point += point;

			await _historyPointRepository.AddAsync(new Domain.Entities.HistoryPoint
			{
				Id = 0,
				CreatedAt = DateTime.Now,
				Content = "Đánh giá đặt lịch " + booking.PaymentCode,
				Point = point,
				UserID = booking.UserID,
			});

			await _feedbackRepository.AddAsync(feedback);

			var result = await _feedbackRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Create new Feedback fail!");

			return "Create Feedback successfully";
		}
	}
}
