using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.UpdateResetPassword
{
    public class UpdateResetPasswordCommandHandler : IRequestHandler<UpdateResetPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<UpdateResetPasswordCommandHandler> _logger;
        private readonly IUtilityService _utility;

        public UpdateResetPasswordCommandHandler(IUserRepository userRepository, 
                                                 IAppLogger<UpdateResetPasswordCommandHandler> logger,
                                                 IUtilityService utility)
        {
            _userRepository = userRepository;
            _logger = logger;
           _utility = utility;
        }
        public async Task<string> Handle(UpdateResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateResetPasswordCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Update Reset Password Request", validationResult);

            var user = await _userRepository.Find(x => x.Email == request.Email && x.IsDeleted == false, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.Email);

            if(user.ResetToken != null && !_utility.Verify(request.VerificationCode, user.ResetToken))
                 throw new BadRequestException("Verification code not match");
            
            user.PasswordHash = _utility.Hash(request.NewPassword);
            user.ResetToken = null;
            user.ExpirationResetToken = null;
            
            _userRepository.Update(user);

            var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (result < 0)
                throw new BadRequestException("Update password fail");

            return  "Update password successfully";
        }
    }
}
