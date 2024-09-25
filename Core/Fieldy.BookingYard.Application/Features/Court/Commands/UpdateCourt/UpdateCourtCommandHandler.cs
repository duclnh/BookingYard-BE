using System;
using System.Security.Cryptography.X509Certificates;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Court.Commands.UpdateCourt;

public class UpdateCourtCommandHandler : IRequestHandler<UpdateCourtCommand, string>
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly ICourtRepository _courtRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJWTService _jwtService;
    private readonly IUtilityService _utilityService;

    public UpdateCourtCommandHandler(IFacilityRepository facilityRepository, ICourtRepository courtRepository, IUserRepository userRepository, IJWTService jwtService, IUtilityService utilityService)
    {
        _facilityRepository = facilityRepository;
        _courtRepository = courtRepository;
        _userRepository = userRepository;
        _jwtService = jwtService;
        _utilityService = utilityService;
    }

    public async Task<string> Handle(UpdateCourtCommand request, CancellationToken cancellationToken)
    {
        List<string> oldImages = new List<string>();
        var user = await _userRepository.Find(x => x.Id == _jwtService.UserID, cancellationToken);
        var facility = await _facilityRepository.Find(x => x.UserID == user.Id, cancellationToken);

        if (user == null || facility == null || (user.Role != Role.Admin && facility.UserID != user.Id))
            throw new BadRequestException("You don't have permission");

        var court = await _courtRepository.Find(x => x.Id == request.CourtID
                                                && x.FacilityID == request.FacilityID,
                                                cancellationToken);

        if (court == null)
            throw new NotFoundException(nameof(court), request.CourtID);

        court.CourtName = request.CourtName ?? court.CourtName;
        court.NumberPlayer = request.NumberPlayer ?? court.NumberPlayer;
        court.CourtPrice = request.CourtPrice ?? court.CourtPrice;
        court.SportID = request.SportID ?? court.SportID;
        court.IsActive = request.IsActive ?? court.IsActive;    
        
        if (request.Image != null)
        {
            oldImages.Add(court.Image);
            court.Image = await _utilityService.AddFile(request.Image, "court");
        }
        if (request.Image360 != null)
        {
            oldImages.Add(court.Image360);
            court.Image360 = await _utilityService.AddFile(request.Image360, "360");
        }
        _courtRepository.Update(court);

        var result = await _courtRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        if (result < 0)
            throw new BadRequestException("Update court fail");
       
        _utilityService.RemoveFile(oldImages);

        return "Update court successfully";
    }
}
