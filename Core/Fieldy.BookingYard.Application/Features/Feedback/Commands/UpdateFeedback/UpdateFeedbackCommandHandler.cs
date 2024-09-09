using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
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

			var Feedback = _mapper.Map<Domain.Entities.FeedBack>(request);

			if (Feedback == null)
				throw new BadRequestException("Error Update Feedback!");

			_FeedbackRepository.Update(Feedback);

			var result = await _FeedbackRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Update Feedback fail!");

			return "Update Feedback successfully";
		}
	}
}
