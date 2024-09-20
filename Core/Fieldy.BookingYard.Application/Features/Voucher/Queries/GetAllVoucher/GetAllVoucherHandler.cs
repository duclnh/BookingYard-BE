using AutoMapper;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.GetAllVoucher { 

    public class GetAllVoucherHandler : IRequestHandler<GetAllVoucherQuery, PagingResult<VoucherDTO>>
	{
		private readonly IMapper _mapper;
		private readonly IVoucherRepository _voucherRepository;

        public GetAllVoucherHandler(IMapper mapper, IVoucherRepository voucherRepository)
        {
            _mapper = mapper;
            _voucherRepository = voucherRepository;
        }

        public async Task<PagingResult<VoucherDTO>> Handle(GetAllVoucherQuery request, CancellationToken cancellationToken)
		{

			var listVoucher = await _voucherRepository.FindAllPaging(
				currentPage: request.requestParams.CurrentPage,
				pageSize: request.requestParams.PageSize,
				expression: x => request.requestParams.Search != null
								&& x.VoucherName.ToLower().Contains(request.requestParams.Search.ToLower().Trim()),
				orderBy: x => x.OrderByDescending(x => x.ExpiredDate),
				cancellationToken: cancellationToken);

			return PagingResult<VoucherDTO>.Create(
			   totalCount: listVoucher.TotalCount,
			   pageSize: listVoucher.PageSize,
			   currentPage: listVoucher.CurrentPage,
			   totalPages: listVoucher.TotalPages,
			   hasNext: listVoucher.HasNext,
			   hasPrevious: listVoucher.HasPrevious,
			   results: _mapper.Map<IList<VoucherDTO>>(listVoucher.Results)
		   );
		}
	}
}
