﻿using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Command.CreateVoucher
{
	public class CreateVoucherCommandHandler : IRequestHandler<CreateVoucherCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IVoucherRepository _voucherRepository;
		private readonly IUtilityService _utilityService;

        public CreateVoucherCommandHandler(IMapper mapper, IVoucherRepository voucherRepository, IUtilityService utilityService)
        {
            _mapper = mapper;
            _voucherRepository = voucherRepository;
			_utilityService = utilityService;
        }

        public async Task<string> Handle(CreateVoucherCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateVoucherCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register Voucher", validationResult);

			var voucherExist = await _voucherRepository.Find(x => x.VoucherName == request.VoucherName, cancellationToken);

			if (voucherExist != null)
				throw new ConflictException("Voucher already exists");

			var voucher = _mapper.Map<Domain.Entities.Voucher>(request);

			voucher.Image = await _utilityService.AddFile(request.Image, "voucher");
			voucher.Status = true;	

			if (voucher == null)
				throw new BadRequestException("Error create Voucher!");

			await _voucherRepository.AddAsync(voucher);

			var result = await _voucherRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Create new Voucher fail!");

			return "Create Discount successfully";
		}
	}
}