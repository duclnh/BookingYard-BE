using System;
using AutoMapper;
using Fieldy.BookingYard.Application.Features.Court.Queries.DTO;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Court.Queries.GetAllCourtBooking;

public class GetAllCourtBookingQueryHandler : IRequestHandler<GetAllCourtBookingQuery, IList<CourtBookingDTO>>
{
    private readonly IMapper _mapper;
    private readonly ICourtRepository _courtRepository;
    private readonly IBookingRepository _bookingRepository;

    public GetAllCourtBookingQueryHandler(IMapper mapper, ICourtRepository courtRepository, IBookingRepository bookingRepository)
    {
        _mapper = mapper;
        _courtRepository = courtRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<IList<CourtBookingDTO>> Handle(GetAllCourtBookingQuery request, CancellationToken cancellationToken)
    {
        var courts = await _courtRepository.FindAll(
            expression: x => x.FacilityID == request.facilityID
                            && x.SportID == request.sportID
                            && x.IsDelete == false
                            && x.IsActive == true,
            cancellationToken: cancellationToken

        );

        TimeSpan.TryParse(request.startTime, out var startTime);
        TimeSpan.TryParse(request.endTime, out var endTime);

        var availableCourts = new List<Domain.Entities.Court>();

        foreach (var court in courts)
        {
            var bookings = await _bookingRepository.AnyAsync(
                x => x.CourtID == court.Id && x.BookingDate == request.playDate && (
                    (x.StartTime < endTime && x.EndTime > startTime)
                ),
                cancellationToken: cancellationToken
            );

            if (!bookings)
            {
                availableCourts.Add(court);
            }
        }

        return _mapper.Map<IList<CourtBookingDTO>>(availableCourts);
    }
}
