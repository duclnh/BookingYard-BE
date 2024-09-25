using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.DeleteFeedback
{
	public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IFeedbackRepository _FeedbackRepository;
		public DeleteFeedbackCommandHandler(IMapper mapper, IFeedbackRepository FeedbackRepository)
		{
			_mapper = mapper;
			_FeedbackRepository = FeedbackRepository;
		}
		public async Task<string> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
		{
			var Feedback = _mapper.Map<Domain.Entities.FeedBack>(request);

			if (Feedback == null)
				throw new BadRequestException("Error Delete Feedback!");

			_FeedbackRepository.Remove(Feedback);

			var result = await _FeedbackRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Delete Feedback fail!");

			return "Delete Feedback successfully";
		}
	}
}
