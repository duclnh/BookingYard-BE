using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.CollectVoucher.Commands.UpdateCollectVoucher
{
	public class UpdateCollectVoucherCommandHandler : IRequestHandler<UpdateCollectVoucherCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly ICollectVoucherRepository _CollectVoucherRepository;
		public UpdateCollectVoucherCommandHandler(IMapper mapper, ICollectVoucherRepository CollectVoucherRepository)
		{
			_mapper = mapper;
			_CollectVoucherRepository = CollectVoucherRepository;
		}
		public async Task<string> Handle(UpdateCollectVoucherCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateCollectVoucherCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid CollectVoucher", validationResult);

			var CollectVoucher = await _CollectVoucherRepository.Find(x => x.Id == request.CollectVoucherID, cancellationToken);
			if (CollectVoucher == null)
				throw new NotFoundException(nameof(CollectVoucher), request.CollectVoucherID);

			CollectVoucher.IsUsed = true;
			_CollectVoucherRepository.Update(CollectVoucher);

			var result = await _CollectVoucherRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
			if (result < 0)
				throw new BadRequestException("Update new collect voucher fail!");

			return "Update cvollect voucher successfully";
		}
	}
}
