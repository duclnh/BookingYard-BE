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

        facility.Image = await _utilityService.AddFile(request.Image, "facility");

        if (request.Logo != null)
        {
            facility.Logo = await _utilityService.AddFile(request.Logo, "facility");
        }

        List<Domain.Entities.Image> images = new List<Domain.Entities.Image>();


        if (request.Other != null)
        {
            foreach (var file in request.Other)
            {
                var image = new Image
                {
                    Id = 0,
                    ImageLink = await _utilityService.AddFile(file, "facility"),
                    Facility = facility,
                };
                images.Add(image);
            }
        }

        facility.Images = images;

        List<FacilityTime> facilityTimes = new List<FacilityTime>();

        foreach (var time in request.OpenDate)
        {

            var facilityTime = new FacilityTime
            {
                Id = 0,
                Time = time,
            };

            facilityTimes.Add(facilityTime);

        }

        facility.FacilityTimes = facilityTimes;

        List<PeakHour> peakHours = new List<PeakHour>();
        if (request.PeakHour != null)
        {
            foreach (var time in request.PeakHour)
            {
                var peakHour = new PeakHour
                {
                    Time = time,
                    Id = 0,
                };
                peakHours.Add(peakHour);
            }
        }

        facility.PeakHours = peakHours;

        List<Holiday> holidays = new List<Holiday>();
        if (request.HolidayDate != null)
        {
            foreach (var date in request.HolidayDate)
            {
                var holidayDate = new Holiday
                {
                    Id = 0,
                    Date = date
                };
                holidays.Add(holidayDate);
            }
        }

        facility.Holidays = holidays;

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
