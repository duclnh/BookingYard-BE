using AutoMapper;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.GetAllVoucherFacility;

public class GetAllVoucherFacilityQueryHandler : IRequestHandler<GetAllVoucherFacilityQuery, PagingResult<VoucherDTO>>
{
    private readonly IVoucherRepository _voucherRepository;
    private readonly IMapper _mapper;

    public GetAllVoucherFacilityQueryHandler(IVoucherRepository voucherRepository, IMapper mapper)
    {
        _voucherRepository = voucherRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<VoucherDTO>> Handle(GetAllVoucherFacilityQuery request, CancellationToken cancellationToken)
    {
        var vouchers = await _voucherRepository.FindAllPaging(
            currentPage: request.requestParams.CurrentPage,
            pageSize: request.requestParams.PageSize,
            expression: x => x.FacilityID == request.facilityID,
            cancellationToken: cancellationToken
        );

        return PagingResult<VoucherDTO>.Create(
               totalCount: vouchers.TotalCount,
               pageSize: vouchers.PageSize,
               currentPage: vouchers.CurrentPage,
               totalPages: vouchers.TotalPages,
               hasNext: vouchers.HasNext,
               hasPrevious: vouchers.HasPrevious,
               results: _mapper.Map<IList<VoucherDTO>>(vouchers.Results)
        );
    }
}
