using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.CollectVoucher.Commands.CreateCollectVoucher
{
	public class CreateCollectVoucherCommandHandler : IRequestHandler<CreateCollectVoucherCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly ICollectVoucherRepository _collectVoucherRepository;

		public CreateCollectVoucherCommandHandler(IMapper mapper, ICollectVoucherRepository collectVoucherRepository)
		{
			_mapper = mapper;
			_collectVoucherRepository = collectVoucherRepository;
		}

		public async Task<string> Handle(CreateCollectVoucherCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateCollectVoucherCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid Collect Voucher", validationResult);

			var collectVoucher = _mapper.Map<Domain.Entities.CollectVoucher>(request);
			collectVoucher.IsUsed = false;
			collectVoucher.CreatedAt = DateTime.Now;

			if (collectVoucher == null)
				throw new BadRequestException("Error create collect voucher!");

			await _collectVoucherRepository.AddAsync(collectVoucher);

			var result = await _collectVoucherRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Create new collect voucher fail!");

			return "Create Collect Voucher successfully";
		}
	}
}
