using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Features.FacilityTime.Commands.UpdateFacilityTime;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.FacilityTime.Commands.UpdateFacilityTime
{
	internal class UpdateFacilityTimeCommandHandler : IRequestHandler<UpdateFacilityTimeCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IFacilityTimeRepository _FacilityTimeRepository;
		public UpdateFacilityTimeCommandHandler(IMapper mapper, IFacilityTimeRepository FacilityTimeRepository)
		{
			_mapper = mapper;
			_FacilityTimeRepository = FacilityTimeRepository;
		}
		public async Task<string> Handle(UpdateFacilityTimeCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateFacilityTimeCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register FacilityTime", validationResult);

			var FacilityTime = _mapper.Map<Domain.Entities.FacilityTime>(request);

			if (FacilityTime == null)
				throw new BadRequestException("Error Update FacilityTime!");

			_FacilityTimeRepository.Update(FacilityTime);

			var result = await _FacilityTimeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Update FacilityTime fail!");

			return "Update FacilityTime successfully";
		}
	}
}
