using System;
using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Enum;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Court.Commands.CreateCourt;

public class CreateCourtCommandHandler : IRequestHandler<CreateCourtCommand, string>
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly ICourtRepository _courtRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJWTService _jwtService;

    public CreateCourtCommandHandler(IFacilityRepository facilityRepository, ICourtRepository courtRepository, IUserRepository userRepository, IJWTService jWTService)
    {
        _facilityRepository = facilityRepository;
        _courtRepository = courtRepository;
        _userRepository = userRepository;
        _jwtService = jWTService;
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



        return "Create court successfully";
    }
}
