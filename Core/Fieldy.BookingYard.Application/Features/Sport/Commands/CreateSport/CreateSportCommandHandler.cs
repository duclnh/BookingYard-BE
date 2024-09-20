using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Sport.Commands.CreateSport;

public class CreateSportCommandHandler : IRequestHandler<CreateSportCommand, string>
{
    private readonly ISportRepository _sportRepository;
    private readonly IUtilityService _utilityService;
    private readonly IMapper _mapper;

    public CreateSportCommandHandler(ISportRepository sportRepository, IUtilityService utilityService, IMapper mapper)
    {
        _sportRepository = sportRepository;
        _utilityService = utilityService;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateSportCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateSportCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid create sport", validationResult);

        var sportExist = await _sportRepository.AnyAsync(x=> x.SportName.ToLower().Trim() == request.Name.ToLower().Trim());
        if (sportExist)
            throw new ConflictException("This name sport already exist!");

        var sport = _mapper.Map<Domain.Entities.Sport>(request);
        if (request.Image != null)
        {
            sport.Image = await _utilityService.AddFile(request.Image, "sport");
        }
        sport.CreateAt = DateTime.Now;

        await _sportRepository.AddAsync(sport);
        var result = await _sportRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return "Create sport successfully";
    }
}
