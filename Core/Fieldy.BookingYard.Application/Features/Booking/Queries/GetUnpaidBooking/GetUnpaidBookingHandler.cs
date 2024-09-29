using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Application.Features.Booking.Queries.GetBookingDetail;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetUnpaidBooking
{
	public class GetUnpaidBookingHandler : IRequestHandler<GetUnpaidBooking, BookingDetailDto>
	{
		private readonly IBookingRepository _bookingRepository;
		private readonly IMapper _mapper;
		public GetUnpaidBookingHandler(IBookingRepository bookingRepository, IMapper mapper)
		{
			_bookingRepository = bookingRepository;
			_mapper = mapper;
		}
		public async Task<BookingDetailDto> Handle(GetUnpaidBooking request, CancellationToken cancellationToken)
		{
			var booking = await _bookingRepository.Find(x => x.PaymentStatus == false && x.CustomerID == x.CustomerID, cancellationToken);
			if (booking == null)
				throw new NotFoundException(nameof(booking), request.userID);

			var bookingMapped = _mapper.Map<BookingDetailDto>(booking);
			return bookingMapped;
		}
	}
}
