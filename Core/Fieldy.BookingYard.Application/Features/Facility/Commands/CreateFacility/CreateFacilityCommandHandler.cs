using AutoMapper;
using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models;
using Fieldy.BookingYard.Domain.Entities;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Commands.CreateFacility;

public class CreateFacilityCommandHandler : IRequestHandler<CreateFacilityCommand, string>
{
    private readonly IFacilityRepository _facilityRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUtilityService _utilityService;
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;

    public CreateFacilityCommandHandler(IFacilityRepository facilityRepository,
                                        IUserRepository userRepository,
                                        IEmailSender emailSender,
                                        IMapper mapper,
                                        IUtilityService utilityService)
    {
        _facilityRepository = facilityRepository;
        _userRepository = userRepository;
        _utilityService = utilityService;
        _emailSender = emailSender;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateFacilityCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateFacilityCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid register facility", validationResult);

        var user = _mapper.Map<Domain.Entities.User>(request);

        var facility = _mapper.Map<Domain.Entities.Facility>(request);

        if (user == null)
            throw new BadRequestException("Error map user");

        var EmailExist = await _userRepository.AnyAsync(x => x.Email == user.Email
                                                    && x.IsDeleted == false);

        if (EmailExist)
            throw new ConflictException("Email already exist!");


        user.PasswordHash = _utilityService.Hash("Fieldy123456");
        user.Role = Domain.Enum.Role.CourtOwner;
        user.Gender = Domain.Enum.Gender.Other;
        user.UserName = user.Email.Split("@")[0];

        var UserNameExist = await _userRepository.AnyAsync(x => x.UserName == user.UserName
                                                            && x.IsDeleted == false);
        if (UserNameExist)
            throw new ConflictException("Username already exist!");

        facility.User = user;
        facility.IsActive = true;

        facility.Image = await _utilityService.AddFile(request.Image, "facility");

        if (request.Logo != null)
        {
            facility.Logo = await _utilityService.AddFile(request.Logo, "facility");
        }

        if (request.Other?.Any() == true)
        {
            facility.Images = request.Other
                                     .Select(async file => new Image
                                     {
                                        Id = 0,
                                        ImageLink = await _utilityService.AddFile(file, "facility"),
                                     })
                                     .Select(t => t.Result)
                                     .ToList();
        }

        facility.FacilityTimes = request.OpenDate
                                        .Select(time => new FacilityTime{
                                            Id = 0,
                                            Time = time
                                        })
                                        .ToList();

        if (request.PeakHour?.Any() == true)
        {
            facility.PeakHours = request.PeakHour
                                        .Select(x => new PeakHour{
                                            Id =  0,
                                            Time = x
                                        }).ToList();
        }

        if (request.HolidayDate?.Any() == true)
        {
            facility.Holidays  = request.HolidayDate
                                        .Select(x => new Holiday{
                                            Id = 0,
                                            Date = x
                                        }).ToList();
        }
        
        await _facilityRepository.AddAsync(facility);

        var result = await _facilityRepository.UnitOfWork.SaveChangesAsync();
        if (result < 0)
            throw new BadRequestException("Create facility fail");

        EmailMessage email = new()
        {
            To = user.Email,
            Subject = "Tạo mới cơ sở",
            Body = "",
        };

        await _emailSender.SendEmailAsync(email);

        return "Create facility successfully";
    }
}
