using AutoMapper;
using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Commands.CreateFacility;

public class CreateFacilityCommandHandler : IRequestHandler<CreateFacilityCommand, string>
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUtilityService _utilityService;
    private readonly IMapper _mapper;

    public CreateFacilityCommandHandler(IFacilityRepository facilityRepository, 
                                        IUserRepository userRepository, 
                                        IMapper mapper,
                                        IUtilityService utilityService)
    {
        _facilityRepository = facilityRepository;
        _userRepository = userRepository;
        _utilityService = utilityService;
        _mapper = mapper;   
    }

    public async Task<string> Handle(CreateFacilityCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateFacilityCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid register facility", validationResult);

        var user = new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = _utilityService.Hash("FieldyFacility"),
            Name = request.Name,
            Phone = request.Phone,
            Gender = Domain.Enum.Gender.Other,
            Role = Domain.Enum.Role.CourtOwner,
        };

        await _userRepository.AddAsync(user);


        return "Create facility successfully";
    }
}
