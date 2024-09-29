using AutoMapper;
using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Application.Features.Feedback.Queries;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetAllBookingCustomer
{
	public class GetAllBookingCustomerHandler : IRequestHandler<GetAllBookingCustomerQuery, PagingResult<BookingDetailDto>>
	{
		private readonly IBookingRepository _bookingRepository;
		private readonly IMapper _mapper;

		public GetAllBookingCustomerHandler(IBookingRepository bookingRepository, IMapper mapper)
		{
			_bookingRepository = bookingRepository;
			_mapper = mapper;
		}

		public async Task<PagingResult<BookingDetailDto>> Handle(GetAllBookingCustomerQuery request, CancellationToken cancellationToken)
		{
			var listBooking = await _bookingRepository.FindAllPaging(
				currentPage: request.requestParams.CurrentPage,
				pageSize: Math.Min(request.requestParams.PageSize, 10),
				expression: x => x.UserID == request.userId,
				orderBy: x => x.OrderByDescending(x => x.CreatedAt),
				cancellationToken: cancellationToken);

			return PagingResult<BookingDetailDto>.Create(
			   totalCount: listBooking.TotalCount,
			   pageSize: listBooking.PageSize,
			   currentPage: listBooking.CurrentPage,
			   totalPages: listBooking.TotalPages,
			   hasNext: listBooking.HasNext,
			   hasPrevious: listBooking.HasPrevious,
			   results: _mapper.Map<IList<BookingDetailDto>>(listBooking.Results)
		   );
		}
	}
}
