using System.Linq.Expressions;
using AutoMapper;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Voucher.Queries.GetAllVoucher
{

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

			List<Expression<Func<Domain.Entities.Voucher, bool>>> expressions = new List<Expression<Func<Domain.Entities.Voucher, bool>>>{
				x => x.FacilityID == null
			};

			if (!string.IsNullOrEmpty(request.requestParams.Search))
			{
				string search = request.requestParams.Search.Trim().ToLower();
				expressions.Add(
					x => x.VoucherName.ToLower().Contains(search)
				);
			}

			switch (request.OrderBy)
			{
				case "stock":
					expressions.Add(
						 x => x.Quantity > 0
					 );
					break;

				case "outStock":
					expressions.Add(
						 x => x.Quantity == 0
					 );
					break;

				case "isDeleted":
					expressions.Add(
					 x => x.IsDeleted
				 );
					break;
			}

			Expression<Func<Domain.Entities.Voucher, bool>>[] expressionArray = expressions.ToArray();

			var vouchers = await _voucherRepository.FindAllPaging(
				currentPage: request.requestParams.CurrentPage,
				pageSize: request.requestParams.PageSize,
				expressions: expressionArray,
				orderBy: x => x.OrderByDescending(x => x.CreatedAt),
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
}
