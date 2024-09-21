using AutoMapper;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Queries;
using Fieldy.BookingYard.Application.Features.CollectVoucher.Queries.GetAllVoucher;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.CollectcollectVoucher.Queries.GetAllcollectVoucher
{
	public class GetAllCollectVoucherHandler : IRequestHandler<GetAllCollectVoucherQuery, PagingResult<CollectVoucherDto>>
	{
		private readonly IMapper _mapper;
		private readonly ICollectVoucherRepository _collectVoucherRepository;

		public GetAllCollectVoucherHandler(IMapper mapper, ICollectVoucherRepository collectVoucherRepository)
		{
			_mapper = mapper;
			_collectVoucherRepository = collectVoucherRepository;
		}

		public async Task<PagingResult<CollectVoucherDto>> Handle(GetAllCollectVoucherQuery request, CancellationToken cancellationToken)
		{

			var listcollectVoucher = await _collectVoucherRepository.FindAllPaging(
				currentPage: request.requestParams.CurrentPage,
				pageSize: request.requestParams.PageSize,
				expression: x => x.UserID == request.UserID,
				orderBy: x => x.OrderByDescending(x => x.CreatedAt),
				cancellationToken: cancellationToken);

			return PagingResult<CollectVoucherDto>.Create(
			   totalCount: listcollectVoucher.TotalCount,
			   pageSize: listcollectVoucher.PageSize,
			   currentPage: listcollectVoucher.CurrentPage,
			   totalPages: listcollectVoucher.TotalPages,
			   hasNext: listcollectVoucher.HasNext,
			   hasPrevious: listcollectVoucher.HasPrevious,
			   results: _mapper.Map<IList<CollectVoucherDto>>(listcollectVoucher.Results)
		   );
		}
	}
}
