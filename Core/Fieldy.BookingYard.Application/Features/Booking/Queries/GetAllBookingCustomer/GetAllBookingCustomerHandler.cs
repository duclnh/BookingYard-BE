using AutoMapper;
using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Application.Features.Feedback.Queries;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetAllBookingCustomer
{
	public class GetAllBookingCustomerHandler : IRequestHandler<GetAllBookingCustomerQuery, PagingResult<CustomerBooking>>
	{
		private readonly IBookingRepository _bookingRepository;
		private readonly IMapper _mapper;

		public GetAllBookingCustomerHandler(IBookingRepository bookingRepository, IMapper mapper)
		{
			_bookingRepository = bookingRepository;
			_mapper = mapper;
		}

		public async Task<PagingResult<CustomerBooking>> Handle(GetAllBookingCustomerQuery request, CancellationToken cancellationToken)
		{
			var listBooking = await _bookingRepository.FindAllPaging(
				currentPage: request.requestParams.CurrentPage,
				pageSize: Math.Min(request.requestParams.PageSize, 10),
				expression: x => x.UserID == request.userId,
				orderBy: x => x.OrderByDescending(x => x.CreatedAt),
				cancellationToken: cancellationToken,
				includes: new Expression<Func<Domain.Entities.Booking, object>>[]
				{
					x => x.Court,
					x => x.Court.Facility,
				});

			return PagingResult<CustomerBooking>.Create(
			   totalCount: listBooking.TotalCount,
			   pageSize: listBooking.PageSize,
			   currentPage: listBooking.CurrentPage,
			   totalPages: listBooking.TotalPages,
			   hasNext: listBooking.HasNext,
			   hasPrevious: listBooking.HasPrevious,
			   results: _mapper.Map<IList<CustomerBooking>>(listBooking.Results)
		   );
		}
	}
}
