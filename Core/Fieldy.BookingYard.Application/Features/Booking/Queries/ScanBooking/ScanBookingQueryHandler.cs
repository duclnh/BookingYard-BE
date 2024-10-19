using System;
using System.Linq.Expressions;
using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Features.Booking.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Booking.Queries.ScanBooking;

public class ScanBookingQueryHandler : IRequestHandler<ScanBookingQuery, PagingResult<BookingDetailDto>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public ScanBookingQueryHandler(IBookingRepository bookingRepository, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<BookingDetailDto>> Handle(ScanBookingQuery request, CancellationToken cancellationToken)
    {
        var bookings = await _bookingRepository.FindAllPaging(
                currentPage: request.RequestParams.CurrentPage,
                pageSize: Math.Min(request.RequestParams.PageSize, 10),
                expression: x => (x.PaymentCode == request.Code || x.Email == request.Email || x.Phone == request.Phone)
                                    && x.Court.FacilityID == request.FacilityID
                                    && !x.IsDeleted,
                orderBy: x => x.OrderByDescending(x => x.CreatedAt),
                cancellationToken: cancellationToken,
                includes: new Expression<Func<Domain.Entities.Booking, object>>[]
                {
                    x => x.Court,
                    x => x.Court.Facility,
                    x => x.Court.Sport,
                    x => x.Voucher,
                });

        if (bookings.Results.Count == 0)
            throw new NotFoundException(nameof(bookings), request.Email ?? request.Phone);

        return PagingResult<BookingDetailDto>.Create(
           totalCount: bookings.TotalCount,
           pageSize: bookings.PageSize,
           currentPage: bookings.CurrentPage,
           totalPages: bookings.TotalPages,
           hasNext: bookings.HasNext,
           hasPrevious: bookings.HasPrevious,
           results: _mapper.Map<IList<BookingDetailDto>>(bookings.Results)
       );
    }
}
