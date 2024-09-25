using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.Verification
{
    public class VerificationCommandHandler : IRequestHandler<VerificationCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<VerificationCommandHandler> _logger;
        private readonly IUtilityService _utility;

        public VerificationCommandHandler(IUserRepository userRepository, 
                                          IAppLogger<VerificationCommandHandler> logger, 
                                          IUtilityService utility)
        {
            _userRepository = userRepository;
            _logger = logger;
            _utility = utility;
        }

        public async Task<string> Handle(VerificationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => x.Id == request.UserID && x.IsDeleted == false, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.UserID);

            if (user.IsVerification())
                throw new BadRequestException($"{user.Email} could not verify a verification code");

            var resultVerify = _utility.Verify(request.VerificationCode, user.VerificationToken!);

            if (!resultVerify)
                throw new BadRequestException($"Invalid verification");

            user.VerificationToken = null;
            _userRepository.Update(user);

            _logger.LogInformation($"{user.Email} verification code");

            var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (result < 0)
                throw new BadRequestException("Verification code fail");

            return  "Verification code successfully";
        }
    }
}
