using System;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Feedback.Commands.CreateFeedbackCourtOwner;

public class CreateFeedbackCourtOwnerCommandHandler : IRequestHandler<CreateFeedbackCourtOwnerCommand, string>
{
    private readonly IFeedbackRepository _feedbackRepository;
    public Task<string> Handle(CreateFeedbackCourtOwnerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
