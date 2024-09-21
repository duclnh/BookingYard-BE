using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Court.Queries.GetAllCourt;

public class GetAllCourtQueryHandler : IRequestHandler<GetAllCourtQuery, IList<CourtDTO>>
{
    private readonly IMapper _mapper;
    private readonly ICourtRepository _courtRepository;
    private readonly IFacilityRepository _facilityRepository;
    private readonly IJWTService _jwtService;
    private readonly IUserRepository _userRepository;

    public GetAllCourtQueryHandler(IMapper mapper,
                                    ICourtRepository courtRepository,
                                    IUserRepository userRepository,
                                    IJWTService jwtService,
                                    IFacilityRepository facilityRepository)
    {
        _mapper = mapper;
        _courtRepository = courtRepository;
        _facilityRepository = facilityRepository;
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<IList<CourtDTO>> Handle(GetAllCourtQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Find(x => x.Id == _jwtService.UserID, cancellationToken);
        var facility = await _facilityRepository.Find(x => x.Id == request.FacilityID, cancellationToken);

        if (user == null || facility == null || (user.Role != Role.Admin && facility.UserID != user.Id))
            throw new BadRequestException("You don't have permission");

        var listCourts = await _courtRepository.GetAllCourts(request.FacilityID, cancellationToken);

        return _mapper.Map<IList<CourtDTO>>(listCourts);
    }
}
