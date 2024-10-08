using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.GetBookingDetail
{
	public class GetBookingDetailHandler : IRequestHandler<GetBookingDetailQuery, BookingDetailDto>
	{
		private readonly IBookingRepository _bookingRepository;
		private readonly IMapper _mapper;
		public GetBookingDetailHandler(IBookingRepository bookingRepository, IMapper mapper)
		{
			_bookingRepository = bookingRepository;
			_mapper = mapper;
		}

		public async Task<BookingDetailDto> Handle(GetBookingDetailQuery request, CancellationToken cancellationToken)
		{
			var booking = await _bookingRepository.FindByIdAsync(request.bookingID,
																cancellationToken,
																x => x.Voucher,
																x => x.Court,
																x => x.Court.Facility,
																x => x.Court.Sport);
			if (booking == null)
				throw new NotFoundException(nameof(booking), request.bookingID);

			var bookingMapped = _mapper.Map<BookingDetailDto>(booking);
			return bookingMapped;
		}
	}
}
