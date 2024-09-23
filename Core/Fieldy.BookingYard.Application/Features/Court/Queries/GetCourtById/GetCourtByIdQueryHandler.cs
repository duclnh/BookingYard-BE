using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Court.Queries.GetCourtById;

public class GetCourtByIdQueryHandler : IRequestHandler<GetCourtByIdQuery, CourtDetailDTO>
{
    private readonly IMapper _mapper;
    private readonly ICourtRepository _courtRepository;
    private readonly IFacilityRepository _facilityRepository;
    private readonly IJWTService _jwtService;
    private readonly IUserRepository _userRepository;

    public GetCourtByIdQueryHandler(IMapper mapper,
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

    public async Task<CourtDetailDTO> Handle(GetCourtByIdQuery request, CancellationToken cancellationToken)
    {

        var court = await _courtRepository.FindByIdAsync(request.courtId, cancellationToken, x => x.Sport);
        if (court == null)
            throw new NotFoundException(nameof(court), request.courtId);
            
        return _mapper.Map<CourtDetailDTO>(court);
    }
}
