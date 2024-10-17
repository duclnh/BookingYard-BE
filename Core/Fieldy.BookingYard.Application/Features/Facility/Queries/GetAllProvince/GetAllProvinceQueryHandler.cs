using System;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.GetAllProvince;

public class GetAllProvinceQueryHandler : IRequestHandler<GetAllProvinceQuery, IList<int>>
{
    private readonly IFacilityRepository _facilityRepository;

    public GetAllProvinceQueryHandler(IFacilityRepository facilityRepository)
    {
        _facilityRepository = facilityRepository;
    }

    public async Task<IList<int>> Handle(GetAllProvinceQuery request, CancellationToken cancellationToken)
    {
        return await _facilityRepository.GetFacilityProvince(cancellationToken);
    }
}
