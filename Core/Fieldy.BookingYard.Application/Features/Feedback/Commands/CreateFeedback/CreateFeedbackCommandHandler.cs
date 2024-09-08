using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedback
{
	public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IFeedbackRepository _FeedbackRepository;
		public CreateFeedbackCommandHandler(IMapper mapper, IFeedbackRepository FeedbackRepository)
		{
			_mapper = mapper;
			_FeedbackRepository = FeedbackRepository;
		}
		public async Task<string> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateFeedbackCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register Feedback", validationResult);

			var Feedback = _mapper.Map<Domain.Entities.FeedBack>(request);

			if (Feedback == null)
				throw new BadRequestException("Error create Feedback!");

			await _FeedbackRepository.AddAsync(Feedback);

			var result = await _FeedbackRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Create new Feedback fail!");

			return "Create Feedback successfully";
		}
	}
}
