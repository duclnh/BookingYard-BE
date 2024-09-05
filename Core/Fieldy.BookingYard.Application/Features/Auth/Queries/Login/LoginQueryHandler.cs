using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models.Auth;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<LoginQueryHandler> _logger;
        private readonly IJWTService _jwtService;
        private readonly IUtilityService _utility;

        public LoginQueryHandler(IUserRepository userRepository,
                                 IAppLogger<LoginQueryHandler> logger,
                                 IUtilityService utility,
                                 IJWTService jwtService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _jwtService = jwtService;
            _utility = utility;
        }

        public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => (x.Phone == request.Email
                                                        || x.Email == request.Email)
                                                        && x.IsDeleted == false, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(user), request.Email);

            if (user.IsBanned)
                throw new BadRequestException("User is banned!");

            if (!_utility.Verify(request.Password, user.PasswordHash))
                throw new BadRequestException("Password incorrect!");

            _logger.LogInformation($"Login Account: {user.Email}");

            return new AuthResponse()
            {
                UserID = user.Id.ToString(),
                ImageUrl = user.ImageUrl,
                Name = user.Name,
                Token = _jwtService.CreateTokenJWT(user),
                Email = user.Email,
                Gender = user.Gender.ToString(),
                Role = user.Role.ToString(),
                IsVerification = user.IsVerification(),
            };
        }
    }
}
