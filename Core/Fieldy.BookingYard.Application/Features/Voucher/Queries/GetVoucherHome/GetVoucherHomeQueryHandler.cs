using System;
using AutoMapper;
using Fieldy.BookingYard.Application.Features.Voucher.Queries.DTO;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.GetVoucherHome;

public class GetVoucherHomeQueryHandler : IRequestHandler<GetVoucherHomeQuery, IList<VoucherHomeDTO>>
{
    private readonly IMapper _mapper;
    private readonly IVoucherRepository _voucherRepository;

    public GetVoucherHomeQueryHandler(IMapper mapper, IVoucherRepository voucherRepository)
    {
        _mapper = mapper;
        _voucherRepository = voucherRepository;
    }

    public async Task<IList<VoucherHomeDTO>> Handle(GetVoucherHomeQuery request, CancellationToken cancellationToken)
    {
        var vouchers = await _voucherRepository.GetVoucherTop(6, cancellationToken);

        return _mapper.Map<IList<VoucherHomeDTO>>(vouchers);
    }
}
