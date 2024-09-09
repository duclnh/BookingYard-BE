using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Holiday.Commands.UpdateHoliday
{
	public class UpdateHolidayCommandHandler : IRequestHandler<UpdateHolidayCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IHolidayRepository _HolidayRepository;
		public UpdateHolidayCommandHandler(IMapper mapper, IHolidayRepository HolidayRepository)
		{
			_mapper = mapper;
			_HolidayRepository = HolidayRepository;
		}
		public async Task<string> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateHolidayCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register Holiday", validationResult);

			var Holiday = _mapper.Map<Domain.Entities.Holiday>(request);

			if (Holiday == null)
				throw new BadRequestException("Error Update Holiday!");

			_HolidayRepository.Update(Holiday);

			var result = await _HolidayRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Update Holiday fail!");

			return "Update Holiday successfully";
		}
	}
}
