using Fieldy.BookingYard.Application.Common;
using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.Verification
{
    public class VerificationCommandHandler : IRequestHandler<VerificationCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<VerificationCommandHandler> _logger;
        private readonly ICommonService _commonService;

        public VerificationCommandHandler(IUserRepository userRepository, IAppLogger<VerificationCommandHandler> logger, ICommonService commonService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _commonService = commonService;
        }

        public async Task<string> Handle(VerificationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(x => x.Id == request.UserID, null, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.UserID);

            if (user.IsVerification())
                throw new BadRequestException($"{user.Email} could not verify a verification code");

            var result = _commonService.Verify(request.VerificationCode, user.VerificationToken!);

            if (!result)
                throw new BadRequestException($"Invalid verification");

            user.VerificationToken = null;
            _userRepository.Update(user);

            _logger.LogInformation($"{user.Email} verification code");

            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Verification code successfully" : "Verification code fail";
        }
    }
}
