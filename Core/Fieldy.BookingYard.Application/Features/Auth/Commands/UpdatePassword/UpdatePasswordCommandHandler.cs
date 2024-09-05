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
        private readonly IUtilityService _utility;

        public UpdatePasswordCommandHandler(IUserRepository userRepository,
                                            IAppLogger<UpdatePasswordCommand> logger,
                                            IUtilityService utility)
        {
            _userRepository = userRepository;
            _logger = logger;
            _utility = utility;
        }
        public async Task<string> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePasswordCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Update Password Request", validationResult);

            var user = await _userRepository.Find(x => x.Id == request.UserID && x.IsDeleted == false , cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.UserID);

            if (user.ResetToken != null || user.ExpirationResetToken != null)
                throw new BadRequestException("PLease verify reset password token before update password");

            if (_utility.Verify(request.OldPassword, user.PasswordHash))
                throw new BadRequestException("Old password not match");

            user.PasswordHash = _utility.Hash(request.NewPassword);

            _userRepository.Update(user);


            var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (result < 0)
                throw new BadRequestException("Update password fail");

            return "Update password successfully";
        }
    }
}
