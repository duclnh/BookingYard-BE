using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Holiday.Commands.CreateHoliday
{
	public class CreateHolidayCommandHandler : IRequestHandler<CreateHolidayCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IHolidayRepository _HolidayRepository;
		public CreateHolidayCommandHandler(IMapper mapper, IHolidayRepository HolidayRepository)
		{
			_mapper = mapper;
			_HolidayRepository = HolidayRepository;
		}
		public async Task<string> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateHolidayCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register Holiday", validationResult);

			var Holiday = _mapper.Map<Domain.Entities.Holiday>(request);

			if (Holiday == null)
				throw new BadRequestException("Error create Holiday!");

			await _HolidayRepository.AddAsync(Holiday);

			var result = await _HolidayRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Create new Holiday fail!");

			return "Create Holiday successfully";
		}
	}
}
