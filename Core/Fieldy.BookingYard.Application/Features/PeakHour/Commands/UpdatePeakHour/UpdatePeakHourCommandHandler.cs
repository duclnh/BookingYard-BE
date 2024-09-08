using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.PeakHour.Commands.UpdatePeakHour
{
	public class UpdatePeakHourCommandHandler : IRequestHandler<UpdatePeakHourCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IPeakHourRepository _PeakHourRepository;
		public UpdatePeakHourCommandHandler(IMapper mapper, IPeakHourRepository PeakHourRepository)
		{
			_mapper = mapper;
			_PeakHourRepository = PeakHourRepository;
		}
		public async Task<string> Handle(UpdatePeakHourCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdatePeakHourCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register PeakHour", validationResult);

			var PeakHour = _mapper.Map<Domain.Entities.PeakHour>(request);

			if (PeakHour == null)
				throw new BadRequestException("Error Update PeakHour!");

			_PeakHourRepository.Update(PeakHour);

			var result = await _PeakHourRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Update PeakHour fail!");

			return "Update PeakHour successfully";
		}
	}
}
