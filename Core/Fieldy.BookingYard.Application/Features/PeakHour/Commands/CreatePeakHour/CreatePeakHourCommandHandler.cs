using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.PeakHour.Commands.CreatePeakHour
{
	public class CreatePeakHourCommandHandler : IRequestHandler<CreatePeakHourCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IPeakHourRepository _PeakHourRepository;
		public CreatePeakHourCommandHandler(IMapper mapper, IPeakHourRepository PeakHourRepository)
		{
			_mapper = mapper;
			_PeakHourRepository = PeakHourRepository;
		}
		public async Task<string> Handle(CreatePeakHourCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreatePeakHourCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register PeakHour", validationResult);

			var PeakHour = _mapper.Map<Domain.Entities.PeakHour>(request);

			if (PeakHour == null)
				throw new BadRequestException("Error create PeakHour!");

			await _PeakHourRepository.AddAsync(PeakHour);

			var result = await _PeakHourRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Create new PeakHour fail!");

			return "Create PeakHour successfully";
		}
	}
}
