using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models.Auth;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Queries.LoginAdmin
{
    public class LoginAdminQueryHandler : IRequestHandler<LoginAdminQuery, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<LoginAdminQueryHandler> _logger;
        private readonly IJWTService _jwtService;
        private readonly IUtilityService _utility;

        public LoginAdminQueryHandler(IUserRepository userRepository,
                                 IAppLogger<LoginAdminQueryHandler> logger,
                                 IUtilityService utility,
                                 IJWTService jwtService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _jwtService = jwtService;
            _utility = utility;
        }

        public async Task<AuthResponse> Handle(LoginAdminQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => (x.UserName == request.UserName)
                                                        && x.IsDeleted == false, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(user), request.UserName);

            if (user.IsBanned)
                throw new BadRequestException("User is banned!");

            if (!_utility.Verify(request.Password, user.PasswordHash))
                throw new BadRequestException("Password incorrect!");

            _logger.LogInformation($"Login Admin Account: {user.Email}");

            var jwtResult = _jwtService.CreateTokenJWT(user);
           
            return new AuthResponse()
            {
                UserID = user.Id.ToString(),
                ImageUrl = user.ImageUrl,
                Name = user.Name,
                Token = jwtResult.Token,
                Expiration = jwtResult.Expiration,
                Email = user.Email,
                Gender = user.Gender.ToString(),
                Role = user.Role.ToString(),
                IsVerification = user.IsVerification(),
            };
        }
    }
}
