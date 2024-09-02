using Fieldy.BookingYard.Application.Account;
using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.UpdateResetPassword
{
    public class UpdateResetPasswordCommandHandler : IRequestHandler<UpdateResetPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<UpdateResetPasswordCommandHandler> _logger;
        private readonly IAccountService _accountService;

        public UpdateResetPasswordCommandHandler(
            IUserRepository userRepository, 
            IAppLogger<UpdateResetPasswordCommandHandler> logger,
            IAccountService accountService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _accountService = accountService;
        }
        public async Task<string> Handle(UpdateResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateResetPasswordCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Update Reset Password Request", validationResult);

            var user = await _userRepository.Get(x => x.Email == request.Email, null, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.Email);

            if(user.ResetToken != null && !_accountService.Verify(request.VerificationCode, user.ResetToken))
                 throw new BadRequestException("Verification code not match");
            
            user.PasswordHash = _accountService.Hash(request.NewPassword);
            user.ResetToken = null;
            user.ExpirationResetToken = null;
            
            _userRepository.Update(user);

            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update password successfully" : "Update password fail";
        }
    }
}
