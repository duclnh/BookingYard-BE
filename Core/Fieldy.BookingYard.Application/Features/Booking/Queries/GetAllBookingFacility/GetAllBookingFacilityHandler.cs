using AutoMapper;
using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Application.Features.Booking.Queries.GetAllBookingCustomer;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;
using System.Linq.Expressions;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetAllBookingFacility
{
	public class GetAllBookingFacilityHandler : IRequestHandler<GetAllBookingFacilityQuery, PagingResult<BookingDetailDto>>
	{
		private readonly IBookingRepository _bookingRepository;
		private readonly IMapper _mapper;

		public GetAllBookingFacilityHandler(IBookingRepository bookingRepository, IMapper mapper)
		{
			_bookingRepository = bookingRepository;
			_mapper = mapper;
		}

		public async Task<PagingResult<BookingDetailDto>> Handle(GetAllBookingFacilityQuery request, CancellationToken cancellationToken)
		{
			List<Expression<Func<Domain.Entities.Booking, bool>>> expressions = new List<Expression<Func<Domain.Entities.Booking, bool>>>
			{
				x => x.Court != null && x.Court.FacilityID == request.facilityId
			};

			if (!string.IsNullOrEmpty(request.sportID))
			{
				expressions.Add(x => x.Court.SportID.ToString() == request.sportID);
			}

			if (!string.IsNullOrEmpty(request.requestParams.Search))
			{
				var search = request.requestParams.Search.ToLower().Trim();

				expressions.Add(x => x.Court.CourtName.ToLower().Contains(search) || x.FullName.ToLower().Contains(search));

			}

			switch (request.status)
			{
				case "checked":
					expressions.Add(x => x.IsCheckin == false	);
					break;
				case "paid":
					expressions.Add(x => x.PaymentStatus);
					break;
				case "notPayment":
					expressions.Add(x => x.PaymentStatus == false);
					break;
				case "cancel":
					expressions.Add(x => x.IsDeleted == true);
					break;
			}
			Expression<Func<Domain.Entities.Booking, bool>>[] expressionArray = expressions.ToArray();

			var listBooking = await _bookingRepository.FindAllPaging(
				currentPage: request.requestParams.CurrentPage,
				pageSize: Math.Min(request.requestParams.PageSize, 10),
				expressions: expressionArray,
				orderBy: x => x.OrderByDescending(x => x.CreatedAt),
				cancellationToken: cancellationToken,
				includes: new Expression<Func<Domain.Entities.Booking, object>>[]
				{
					x => x.Court,
					x => x.Court.Facility,
					x => x.Voucher
				});

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
