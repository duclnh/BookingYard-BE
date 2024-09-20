using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Court.Commands.CreateCourt;

public class CreateCourtCommandHandler : IRequestHandler<CreateCourtCommand, string>
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly ICourtRepository _courtRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJWTService _jwtService;
    private readonly IUtilityService _utilityService;

    private readonly IMapper _mapper;

    public CreateCourtCommandHandler(IFacilityRepository facilityRepository,
                                    ICourtRepository courtRepository,
                                    IUserRepository userRepository,
                                    IJWTService jwtService,
                                    IUtilityService utilityService,
                                    IMapper mapper)
    {
        _facilityRepository = facilityRepository;
        _courtRepository = courtRepository;
        _userRepository = userRepository;
        _jwtService = jwtService;
        _utilityService = utilityService;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateCourtCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateCourtCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid register court", validationResult);

        var facility = await _facilityRepository.FindByIdAsync(request.FacilityID, cancellationToken);
        if (facility == null)
            throw new NotFoundException(nameof(facility), request.FacilityID);

        var userCreate = await _userRepository.Find(x => x.Id == _jwtService.UserID, cancellationToken);

        if (userCreate != null && userCreate.Role != Role.Admin && userCreate.Id != facility.UserID)
            throw new BadRequestException($"You don't have permission create");

        var court = _mapper.Map<Domain.Entities.Court>(request);
        court.Image = await _utilityService.AddFile(request.Image, "court");
         
        await _courtRepository.AddAsync(court);
        
        var result = await _courtRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        if (result < 0)
            throw new BadRequestException("Create facility fail");

        return "Create court successfully";
    }
}
