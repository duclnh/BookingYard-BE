using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Domain.Enum;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<UpdatePasswordCommand> _logger;
        private readonly IUtilityService _utility;

        private readonly IJWTService _jWTService;

        public UpdatePasswordCommandHandler(IUserRepository userRepository,
                                            IAppLogger<UpdatePasswordCommand> logger,
                                            IJWTService jWTService,
                                            IUtilityService utility)
        {
            _userRepository = userRepository;
            _logger = logger;
            _utility = utility;
            _jWTService = jWTService;
        }
        public async Task<string> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePasswordCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Update Password Request", validationResult);

            var user = await _userRepository.Find(x => x.Id == request.UserID && x.IsDeleted == false, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.UserID);

            if (!_utility.Verify(request.OldPassword, user.PasswordHash))
                throw new BadRequestException("Old password not match");

            var userUpdate = await _userRepository.Find(x => x.Id == _jWTService.UserID, cancellationToken);
            
            if (userUpdate != null && userUpdate.Role != Role.Admin && userUpdate.Id != user.Id)
                throw new BadRequestException($"You don't have permission update user have ID: {request.UserID}");

            user.PasswordHash = _utility.Hash(request.NewPassword);

            _userRepository.Update(user);


            var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (result < 0)
                throw new BadRequestException("Update password fail");

            return "Update password successfully";
        }
    }
}
