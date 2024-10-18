using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Command.UpdateVoucher
{
	public class UpdateVoucherCommandHandler : IRequestHandler<UpdateVoucherCommand, string>
	{
		private readonly IMapper _mapper;
		private readonly IVoucherRepository _voucherRepository;
		public UpdateVoucherCommandHandler(IMapper mapper, IVoucherRepository voucherRepository)
		{
			_mapper = mapper;
			_voucherRepository = voucherRepository;
		}
		public async Task<string> Handle(UpdateVoucherCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateVoucherCommandValidator();
			var validationResult = await validator.ValidateAsync(request, cancellationToken);
			if (validationResult.Errors.Any())
				throw new BadRequestException("Invalid register Voucher", validationResult);

			var voucherExist = await _voucherRepository.Find(x => x.VoucherName == request.VoucherName && x.Id != request.VoucherID, cancellationToken);
			if (voucherExist != null)
				throw new ConflictException("Voucher already exists");

			voucherExist = await _voucherRepository.Find(x => x.Id == request.VoucherID, cancellationToken);
			if (voucherExist == null)
				throw new NotFoundException(nameof(voucherExist), request.VoucherID);

			voucherExist.VoucherDescription = request.VoucherDescription ?? voucherExist.VoucherDescription;
			voucherExist.Quantity = request.Quantity ?? voucherExist.Quantity;
			voucherExist.Code = request.Code ?? voucherExist.Code;
			voucherExist.ExpiredDate = request.ExpiredDate ?? voucherExist.ExpiredDate;

			voucherExist.VoucherName = request.VoucherName ?? voucherExist.VoucherName;
			voucherExist.Image = request.Image ?? voucherExist.Image;
			voucherExist.Percentage = request.Percentage ?? voucherExist.Percentage;
			voucherExist.RegisterDate = request.RegisterDate ?? voucherExist.RegisterDate;
			voucherExist.Reason = request.Reason ?? voucherExist.Reason;
			voucherExist.Status = request.Status ?? voucherExist.Status;
			voucherExist.SportID = request.SportID == 0 ? null : request.SportID;

			_voucherRepository.Update(voucherExist);
			var result = await _voucherRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

			if (result < 0)
				throw new BadRequestException("Update new Voucher fail!");

			return "Update Voucher successfully";
		}
	}
}
