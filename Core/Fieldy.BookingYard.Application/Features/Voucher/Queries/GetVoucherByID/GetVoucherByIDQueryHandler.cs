using System;
using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Features.Voucher.Queries.DTO;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.GetVoucherByID;

public class GetVoucherByIDQueryHandler : IRequestHandler<GetVoucherByIDQuery, VoucherBookingDTO>
{
    private readonly IMapper _mapper;
    private readonly IVoucherRepository _voucherRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IJWTService _jWTService;

    public GetVoucherByIDQueryHandler(IMapper mapper,
                                        IVoucherRepository voucherRepository,
                                        IBookingRepository bookingRepository,
                                        IJWTService jWTService)
    {
        _mapper = mapper;
        _voucherRepository = voucherRepository;
        _bookingRepository = bookingRepository;
        _jWTService = jWTService;
    }

    public async Task<VoucherBookingDTO> Handle(GetVoucherByIDQuery request, CancellationToken cancellationToken)
    {
        var voucher = await _voucherRepository.Find(x => x.Code == request.voucherCode,
                                                        cancellationToken,
                                                        x => x.Sport,
                                                        x => x.Facility);
        if (voucher == null)
            throw new NotFoundException(nameof(voucher), request.voucherCode);

        var voucherExist = await _bookingRepository.Find(x => x.UserID == _jWTService.UserID && x.VoucherID == voucher.Id && x.IsDeleted == false, cancellationToken);
        if (voucherExist != null)
            throw new BadRequestException("You used this voucher");


        if (voucher.Quantity == 0)
            throw new BadRequestException("Voucher quantity outStock");

        if (voucher.ExpiredDate < DateTime.Now)
            throw new BadRequestException("Voucher expired");

        if (voucher.RegisterDate > DateTime.Now)
            throw new BadRequestException("Voucher not start");

        return _mapper.Map<VoucherBookingDTO>(voucher);
    }
}
