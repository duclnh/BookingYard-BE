using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.PeakHour.Commands.DeletePeakHour
{
	public class DeletePeakHourCommandHandler : IRequestHandler<DeletePeakHourCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IPeakHourRepository _PeakHourRepository;
		public DeletePeakHourCommandHandler(IMapper mapper, IPeakHourRepository PeakHourRepository)
		{
			_mapper = mapper;
			_PeakHourRepository = PeakHourRepository;
		}
		public async Task<string> Handle(DeletePeakHourCommand request, CancellationToken cancellationToken)
		{
			var PeakHour = _mapper.Map<Domain.Entities.PeakHour>(request);

			if (PeakHour == null)
				throw new BadRequestException("Error Delete PeakHour!");

			_PeakHourRepository.Remove(PeakHour);

			var result = await _PeakHourRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Delete PeakHour fail!");

			return "Delete PeakHour successfully";
		}
	}
}
