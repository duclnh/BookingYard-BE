using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.DeleteFeedback
{
	public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, string>
	{
		private readonly IFeedbackRepository _feedbackRepository;
		public DeleteFeedbackCommandHandler(IFeedbackRepository feedbackRepository)
		{
			_feedbackRepository = feedbackRepository;
		}
		public async Task<string> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
		{
			var feedback = await _feedbackRepository.FindByIdAsync(request.FeedbackID, cancellationToken);

			if (feedback == null)
				throw new BadRequestException("Error Delete Feedback!");

			feedback.IsShow = !feedback.IsShow;
			_feedbackRepository.Update(feedback);

			var result = await _feedbackRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Delete Feedback fail!");

			return "Delete Feedback successfully";
		}
	}
}
