using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.CollectVoucher.Commands.CreateCollectVoucher
{
	public class CreateCollectVoucherCommandHandler : IRequestHandler<CreateCollectVoucherCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly ICollectVoucherRepository _collectVoucherRepository;
		private readonly IVoucherRepository _voucherRepository;
		private readonly IJWTService _jwtService;

		public CreateCollectVoucherCommandHandler(IMapper mapper, ICollectVoucherRepository collectVoucherRepository, IVoucherRepository voucherRepository, IJWTService jwtService)
		{
			_mapper = mapper;
			_collectVoucherRepository = collectVoucherRepository;
			_voucherRepository = voucherRepository;
			_jwtService = jwtService;
		}

		public async Task<string> Handle(CreateCollectVoucherCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateCollectVoucherCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid Collect Voucher", validationResult);

			if (_jwtService.UserID != request.UserID)
				throw new BadRequestException("You are not authorized to collect this voucher.");

			var voucher = await _voucherRepository.Find(x => x.Id == request.VoucherID && !x.IsDeleted, cancellationToken);
			if (voucher == null)
				throw new NotFoundException(nameof(voucher), request.VoucherID);

			if (voucher.Quantity == 0)
				throw new BadRequestException("Cannot collect voucher: It has already been fully redeemed.");

			if (voucher.ExpiredDate < DateTime.UtcNow)
				throw new BadRequestException("Cannot collect voucher. This voucher has already expired.");

			var existVoucher = await _collectVoucherRepository.AnyAsync(x => x.VoucherID == request.VoucherID && x.UserID == request.UserID, cancellationToken);
			if (existVoucher)
				throw new ConflictException("You have already collect this voucher before!");


			var collectVoucher = _mapper.Map<Domain.Entities.CollectVoucher>(request);
			collectVoucher.IsUsed = false;
			collectVoucher.CreatedAt = DateTime.Now;

			if (collectVoucher == null)
				throw new BadRequestException("Error create collect voucher!");

			voucher.Quantity -= 1;

			_voucherRepository.Update(voucher);

			await _collectVoucherRepository.AddAsync(collectVoucher);

			var result = await _collectVoucherRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Create new collect voucher fail!");

			return "Create Collect Voucher successfully";
		}
	}
}
