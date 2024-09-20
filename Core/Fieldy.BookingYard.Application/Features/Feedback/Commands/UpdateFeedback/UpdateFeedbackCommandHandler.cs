using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.UpdateFeedback
{
	public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IFeedbackRepository _FeedbackRepository;
		public UpdateFeedbackCommandHandler(IMapper mapper, IFeedbackRepository FeedbackRepository)
		{
			_mapper = mapper;
			_FeedbackRepository = FeedbackRepository;
		}
		public async Task<string> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateFeedbackCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register Feedback", validationResult);

			var Feedback = await _FeedbackRepository.Find(x => x.Id == request.FeedbackID, cancellationToken);

			if (Feedback == null)
				throw new BadRequestException("Error Update Feedback!");

			Feedback.Image = request.Image;
			Feedback.Content = request.Content;
			Feedback.Rating = request.Rating;
			Feedback.TypeFeedback = request.TypeFeedback;
			Feedback.IsShow = request.IsShow;

			_FeedbackRepository.Update(Feedback);

			var result = await _FeedbackRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Update Feedback fail!");

			return "Update Feedback successfully";
		}
	}
}
