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
        private readonly IUtilityService _utility;

        public VerificationResetPasswordCommandHandler(IUserRepository userRepository,
                                                       IAppLogger<VerificationResetPasswordCommandHandler> logger,
                                                       IUtilityService utility)
        {
            _userRepository = userRepository;
            _logger = logger;
            _utility = utility;
        }

        public async Task<string> Handle(VerificationResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => x.Email == request.Email && x.IsDeleted == false, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.Email);

            if (user.ResetToken == null || user.ExpirationResetToken == null)
                throw new BadRequestException($"{user.Email} could not verify a rest password token");

            if (DateTime.Now > user.ExpirationResetToken)
                throw new BadRequestException($"{user.Email} expiration rest token");

            var result = _utility.Verify(request.VerificationCode, user.ResetToken!);

            if (!result)
                throw new BadRequestException($"Invalid verification");

            _logger.LogInformation($"{user.Email} verification reset password");

            return "Verification code successfully";
        }
    }
}
