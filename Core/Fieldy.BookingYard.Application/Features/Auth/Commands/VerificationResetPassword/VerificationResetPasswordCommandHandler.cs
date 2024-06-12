using Fieldy.BookingYard.Application.Common;
using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.VerificationResetPassword
{
    public class VerificationResetPasswordCommandHandler : IRequestHandler<VerificationResetPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<VerificationResetPasswordCommandHandler> _logger;
        private readonly ICommonService _commonService;

        public VerificationResetPasswordCommandHandler(IUserRepository userRepository, IAppLogger<VerificationResetPasswordCommandHandler> logger, ICommonService commonService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _commonService = commonService;
        }

        public async Task<string> Handle(VerificationResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(x => x.Email == request.Email, null, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.Email);

            if (user.ResetToken == null || user.ExpirationResetToken == null)
                throw new BadRequestException($"{user.Email} could not verify a rest password token");

            if (DateTime.Now > user.ExpirationResetToken)
                throw new BadRequestException($"{user.Email} expiration rest token");

            var result = _commonService.Verify(request.VerificationCode, user.ResetToken!);

            if (!result)
                throw new BadRequestException($"Invalid verification");

            _logger.LogInformation($"{user.Email} verification reset password");

            return "Verification code successfully";
        }
    }
}
