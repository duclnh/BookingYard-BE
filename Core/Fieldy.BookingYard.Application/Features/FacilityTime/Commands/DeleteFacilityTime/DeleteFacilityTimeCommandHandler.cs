using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.FacilityTime.Commands.DeleteFacilityTime
{
	public class DeleteFacilityTimeCommandHandler : IRequestHandler<DeleteFacilityTimeCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IFacilityTimeRepository _FacilityTimeRepository;
		public DeleteFacilityTimeCommandHandler(IMapper mapper, IFacilityTimeRepository FacilityTimeRepository)
		{
			_mapper = mapper;
			_FacilityTimeRepository = FacilityTimeRepository;
		}
		public async Task<string> Handle(DeleteFacilityTimeCommand request, CancellationToken cancellationToken)
		{
			var FacilityTime = _mapper.Map<Domain.Entities.FacilityTime>(request);

			if (FacilityTime == null)
				throw new BadRequestException("Error Delete FacilityTime!");

			_FacilityTimeRepository.Remove(FacilityTime);

			var result = await _FacilityTimeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Delete FacilityTime fail!");

			return "Delete FacilityTime successfully";
		}
	}
}
