using System;
using AutoMapper;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.GetAllFacilityPosition;

public class GetAllFacilityPositionQueryHandler : IRequestHandler<GetAllFacilityPositionQuery, IList<FacilityPositionDTO>>
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly IMapper _mapper;

    public GetAllFacilityPositionQueryHandler(IFacilityRepository facilityRepository, IMapper mapper)
    {
        _facilityRepository = facilityRepository;
        _mapper = mapper;
    }

    public async Task<IList<FacilityPositionDTO>> Handle(GetAllFacilityPositionQuery request, CancellationToken cancellationToken)
    {
        var facility = await _facilityRepository.FindAll(x => !x.IsDeleted, cancellationToken: cancellationToken);

        return _mapper.Map<IList<FacilityPositionDTO>>(facility);
    }
}
