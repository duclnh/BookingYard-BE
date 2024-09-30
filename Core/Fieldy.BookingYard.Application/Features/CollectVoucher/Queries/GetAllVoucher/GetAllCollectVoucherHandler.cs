using System.Linq.Expressions;
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
			Expression<Func<Domain.Entities.CollectVoucher, bool>> expression;

			expression = request.type switch
			{
				"outdate" => x => x.UserID == request.UserID && x.Voucher.ExpiredDate < DateTime.Now,
				"notused" => x => x.UserID == request.UserID && !x.IsUsed,
				"used" => x => x.UserID == request.UserID && x.IsUsed,
				_ => x => x.UserID == request.UserID,
			};

			var listCollectVoucher = await _collectVoucherRepository.FindAllPaging(
				currentPage: request.requestParams.CurrentPage,
				pageSize: request.requestParams.PageSize,
				expression: expression,
				orderBy: x => x.OrderByDescending(c => c.CreatedAt),
				cancellationToken: cancellationToken,
				includes: new Expression<Func<Domain.Entities.CollectVoucher, object>>[]
				{
			
					x => x.Voucher,
					x => x.Voucher.Sport,
                    x => x.Voucher.Facility,
                });

			return PagingResult<CollectVoucherDto>.Create(
			   totalCount: listCollectVoucher.TotalCount,
			   pageSize: listCollectVoucher.PageSize,
			   currentPage: listCollectVoucher.CurrentPage,
			   totalPages: listCollectVoucher.TotalPages,
			   hasNext: listCollectVoucher.HasNext,
			   hasPrevious: listCollectVoucher.HasPrevious,
			   results: _mapper.Map<IList<CollectVoucherDto>>(listCollectVoucher.Results)
		   );
		}
	}
}
