using Fieldy.BookingYard.Application.Common;
using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<UpdatePasswordCommand> _logger;
        private readonly ICommonService _commonService;

        public UpdatePasswordCommandHandler(
            IUserRepository userRepository, 
            IAppLogger<UpdatePasswordCommand> logger,
            ICommonService commonService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _commonService = commonService;
        }
        public async Task<string> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePasswordCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Update Password Request", validationResult);

            var user = await _userRepository.Get(x => x.Id == request.UserID, null, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.UserID);

            if (user.ResetToken != null || user.ExpirationResetToken != null)
                throw new BadRequestException("PLease verify reset password token before update password");

            if(_commonService.Verify(request.OldPassword, user.PasswordHash))
                throw new BadRequestException("Old password not match");
                
            user.PasswordHash = _commonService.Hash(request.NewPassword);
            
            _userRepository.Update(user);
            
            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update password successfully" : "Update password fail";
        }
    }
}
