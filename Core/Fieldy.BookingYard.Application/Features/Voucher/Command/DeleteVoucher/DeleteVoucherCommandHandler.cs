using System;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Command.DeleteVoucher;

public class DeleteVoucherCommandHandler : IRequestHandler<DeleteVoucherCommand, string>
{
    private readonly IVoucherRepository _voucherRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IJWTService _jWTService;
    private readonly IUserRepository _userRepository;

    public DeleteVoucherCommandHandler(IVoucherRepository voucherRepository, IBookingRepository bookingRepository, IJWTService jWTService, IUserRepository userRepository)
    {
        _voucherRepository = voucherRepository;
        _bookingRepository = bookingRepository;
        _jWTService = jWTService;
        _userRepository = userRepository;
    }

    public async Task<string> Handle(DeleteVoucherCommand request, CancellationToken cancellationToken)
    {
        var voucherExist = await _voucherRepository.Find(x => x.Id == request.VoucherID, cancellationToken);

        if (voucherExist == null)
            throw new NotFoundException(nameof(voucherExist), request.VoucherID);

        var userUpdate = await _userRepository.Find(x => x.Id == _jWTService.UserID, cancellationToken);
        if (userUpdate == null)
            throw new BadRequestException("User invalid");

        // if (userUpdate.Role != Role.Admin && userUpdate.Id != voucherExist.CreatedBy)
        //     throw new BadRequestException("User invalid");

        var existBookings = await _bookingRepository.AnyAsync(x => x.VoucherID == request.VoucherID && !x.IsDeleted, cancellationToken);
        if (existBookings)
        {
            voucherExist.IsDeleted = true;
            _voucherRepository.Update(voucherExist);
        }
        else
        {
            _voucherRepository.Remove(voucherExist);
        }

        var result = await _voucherRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        if (result < 0)
            throw new BadRequestException("Delete Voucher fail!");

        return "Delete Voucher Successfully";
    }
}
