using System;
using AutoMapper;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.GetFacilityHome;

public class GetFacilityHomeQueryHandler : IRequestHandler<GetFacilityHomeQuery, IList<FacilityHomeDTO>>
{
    private readonly IMapper _mapper;
    private readonly IFacilityRepository _facilityRepository;
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ICourtRepository _courtRepository;

    public GetFacilityHomeQueryHandler(IMapper mapper, IFacilityRepository facilityRepository, IFeedbackRepository feedbackRepository, ICourtRepository courtRepository)
    {
        _mapper = mapper;
        _facilityRepository = facilityRepository;
        _feedbackRepository = feedbackRepository;
        _courtRepository = courtRepository;
    }

    public async Task<IList<FacilityHomeDTO>> Handle(GetFacilityHomeQuery request, CancellationToken cancellationToken)
    {
        var facilities = await _facilityRepository.GetFacilitiesTop(6, cancellationToken);
        var facilityMapped = _mapper.Map<IList<FacilityHomeDTO>>(facilities);
        foreach (var facility in facilityMapped)
        {
            facility.FacilityMinPrice = await _courtRepository.GetMinPriceCourt(facility.FacilityID, cancellationToken);
            facility.FacilityMaxPrice = await _courtRepository.GetMaxPriceCourt(facility.FacilityID, cancellationToken);
            facility.FacilityRating = await _feedbackRepository.GetRatingFacility(facility.FacilityID, cancellationToken);
        }
        return _mapper.Map<IList<FacilityHomeDTO>>(facilityMapped);
    }
}
