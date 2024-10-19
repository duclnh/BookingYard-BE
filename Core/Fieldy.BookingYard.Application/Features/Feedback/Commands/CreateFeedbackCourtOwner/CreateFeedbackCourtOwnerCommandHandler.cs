using System;
using AutoMapper;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedbackCourtOwner;

public class CreateFeedbackCourtOwnerCommandHandler : IRequestHandler<CreateFeedbackCourtOwnerCommand, string>
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IFacilityRepository _facilityRepository;
    private readonly IMapper _mapper;

    public CreateFeedbackCourtOwnerCommandHandler(IFeedbackRepository feedbackRepository, IFacilityRepository facilityRepository, IMapper mapper)
    {
        _feedbackRepository = feedbackRepository;
        _facilityRepository = facilityRepository;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateFeedbackCourtOwnerCommand request, CancellationToken cancellationToken)
    {
        // var validator = new CreateFeedbackCommandValidator();
        // 	var validationResult = await validator.ValidateAsync(request, cancellationToken);
        // 	if (validationResult.Errors.Any())
        // 		throw new BadRequestException("Invalid register Feedback", validationResult);

        var facility = await _facilityRepository.Find(x => x.UserID == request.UserID, cancellationToken);
        if (facility == null)
            throw new NotFoundException(nameof(facility), request.UserID);

        var feedback = await _feedbackRepository.Find(x => x.FacilityID == facility.Id, cancellationToken);
        if (feedback != null)
            throw new BadRequestException("Feedback has already been provided");

        feedback = _mapper.Map<Domain.Entities.FeedBack>(request);
        feedback.IsShow = true;
        feedback.FacilityID = facility.Id;
        feedback.CreatedAt = DateTime.Now;
        feedback.TypeFeedback = TypeFeedback.Owner;

        await _feedbackRepository.AddAsync(feedback);

        var result = await _feedbackRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        if (result < 0)
            throw new BadRequestException("Create new Feedback fail!");

        return "Create Feedback successfully";
    }
}
