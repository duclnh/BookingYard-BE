using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.FacilityTime.Commands.CreateFacilityTime
{
	public class CreateFacilityTimeCommandHandler : IRequestHandler<CreateFacilityTimeCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IFacilityTimeRepository _FacilityTimeRepository;
		public CreateFacilityTimeCommandHandler(IMapper mapper, IFacilityTimeRepository FacilityTimeRepository)
		{
			_mapper = mapper;
			_FacilityTimeRepository = FacilityTimeRepository;
		}
		public async Task<string> Handle(CreateFacilityTimeCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateFacilityTimeCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register FacilityTime", validationResult);

			var FacilityTime = _mapper.Map<Domain.Entities.FacilityTime>(request);

			if (FacilityTime == null)
				throw new BadRequestException("Error create FacilityTime!");

			await _FacilityTimeRepository.AddAsync(FacilityTime);

			var result = await _FacilityTimeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Create new FacilityTime fail!");

			return "Create FacilityTime successfully";
		}
	}
}
