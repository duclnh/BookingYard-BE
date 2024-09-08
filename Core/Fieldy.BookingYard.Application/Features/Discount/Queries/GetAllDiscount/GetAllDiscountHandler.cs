using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Discount.Queries.GetAllDiscount
{
	public class GetAllDiscountHandler : IRequestHandler<GetAllDiscountQuery, PagingResult<DiscountDto>>
	{
		private readonly IMapper _mapper;
		private readonly IDiscountRepository _DiscountRepository;

		public GetAllDiscountHandler(IMapper mapper, IDiscountRepository DiscountRepository)
		{
			_mapper = mapper;
			_DiscountRepository = DiscountRepository;
		}

		public async Task<PagingResult<DiscountDto>> Handle(GetAllDiscountQuery request, CancellationToken cancellationToken)
		{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
			var listDiscount = await _DiscountRepository.FindAllPaging(
				requestParams: request.requestParams,
				expression: x => x.DiscountName.ToLower().Contains(request.requestParams.Search.ToLower().Trim()),
				orderBy: x => x.OrderByDescending(x => x.ExpiredDate),
				cancellationToken: cancellationToken);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

			return PagingResult<DiscountDto>.Create(
			   totalCount: listDiscount.TotalCount,
			   pageSize: listDiscount.PageSize,
			   currentPage: listDiscount.CurrentPage,
			   totalPages: listDiscount.TotalPages,
			   hasNext: listDiscount.HasNext,
			   hasPrevious: listDiscount.HasPrevious,
			   results: _mapper.Map<IList<DiscountDto>>(listDiscount.Results)
		   );
		}
	}
}
