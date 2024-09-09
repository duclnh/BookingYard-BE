using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Holiday.Commands.DeleteHoliday
{
	public class DeleteHolidayCommandHandler : IRequestHandler<DeleteHolidayCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IHolidayRepository _HolidayRepository;
		public DeleteHolidayCommandHandler(IMapper mapper, IHolidayRepository HolidayRepository)
		{
			_mapper = mapper;
			_HolidayRepository = HolidayRepository;
		}
		public async Task<string> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
		{
			var Holiday = _mapper.Map<Domain.Entities.Holiday>(request);

			if (Holiday == null)
				throw new BadRequestException("Error Delete Holiday!");

			_HolidayRepository.Remove(Holiday);

			var result = await _HolidayRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Delete Holiday fail!");

			return "Delete Holiday successfully";
		}
	}
}
