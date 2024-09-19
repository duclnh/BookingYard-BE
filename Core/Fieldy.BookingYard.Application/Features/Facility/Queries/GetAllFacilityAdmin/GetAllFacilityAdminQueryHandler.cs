using System;
using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Models.Paging;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.GetAllFacilityAdmin;

public class GetAllFacilityAdminQueryHandler : IRequestHandler<GetAllFacilityAdminQuery, PagingResult<FacilityAdminDTO>>
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly IMapper _mapper;

    public GetAllFacilityAdminQueryHandler(IFacilityRepository facilityRepository, IMapper mapper)
    {
        _facilityRepository = facilityRepository;
        _mapper = mapper;
    }

    public async Task<PagingResult<FacilityAdminDTO>> Handle(GetAllFacilityAdminQuery request, CancellationToken cancellationToken)
    {
        var facilities = await _facilityRepository.FindAllPaging(
           requestParams: request.requestParams,
           expression: null,
           orderBy: null,
           cancellationToken: cancellationToken,
            x => x.User != null
        );

        return PagingResult<FacilityAdminDTO>.Create(
            totalCount: facilities.TotalCount,
            pageSize: facilities.PageSize,
            currentPage: facilities.CurrentPage,
            totalPages: facilities.TotalPages,
            hasNext: facilities.HasNext,
            hasPrevious: facilities.HasPrevious,
            results: _mapper.Map<IList<FacilityAdminDTO>>(facilities.Results)
        );
    }
}
