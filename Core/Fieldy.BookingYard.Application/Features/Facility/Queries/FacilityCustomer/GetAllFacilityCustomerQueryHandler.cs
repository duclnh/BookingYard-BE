using System;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.FacilityCustomer;

public class GetAllFacilityCustomerQueryHandler : IRequestHandler<GetAllFacilityCustomerQuery, PagingResult<FacilityCustomerDTO>>
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly ICourtRepository _courtRepository;
    private readonly IMapper _mapper;

    public GetAllFacilityCustomerQueryHandler(IFacilityRepository facilityRepository, IFeedbackRepository feedbackRepository, ICourtRepository courtRepository, IMapper mapper)
    {
        _facilityRepository = facilityRepository;
        _feedbackRepository = feedbackRepository;
        _courtRepository = courtRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<FacilityCustomerDTO>> Handle(GetAllFacilityCustomerQuery request, CancellationToken cancellationToken)
    {
        var listFacility = await _facilityRepository.FindAllPaging(
            currentPage: request.request.CurrentPage,
            pageSize: request.request.PageSize,
            expression: x => x.IsDeleted == false,
            cancellationToken: cancellationToken,
            includes: x => x.Courts
        );
        var facilityMapped = _mapper.Map<IList<FacilityCustomerDTO>>(listFacility.Results);

        foreach (var facility in facilityMapped)
        {
            facility.FacilityMinPrice = await _courtRepository.GetMinPriceCourt(facility.FacilityID, cancellationToken);
            facility.FacilityMaxPrice = await _courtRepository.GetMaxPriceCourt(facility.FacilityID, cancellationToken);
            facility.FacilityRating = await _feedbackRepository.GetRatingFacility(facility.FacilityID, cancellationToken);
        }

        return PagingResult<FacilityCustomerDTO>.Create(
               totalCount: listFacility.TotalCount,
               pageSize: listFacility.PageSize,
               currentPage: listFacility.CurrentPage,
               totalPages: listFacility.TotalPages,
               hasNext: listFacility.HasNext,
               hasPrevious: listFacility.HasPrevious,
               results: facilityMapped
           );

    }
}
