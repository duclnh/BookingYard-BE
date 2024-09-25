using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models.Auth;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
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
            var user = await _userRepository.Find(x => (x.Phone == request.UserName
                                                        || x.Email == request.UserName)
                                                        && x.IsDeleted == false, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(user), request.UserName);

            if (!_utility.Verify(request.Password, user.PasswordHash))
                throw new BadRequestException("Password incorrect!");

                if (user.IsBanned)
                throw new BadRequestException("User is banned!");

            if(user.Role != Role.Customer)
                    throw new BadRequestException("Invalid request");

            _logger.LogInformation($"Login Account: {user.Email}");

            var jwtResult = _jwtService.CreateTokenJWT(user);
           
            return new AuthResponse()
            {
                UserID = user.Id.ToString(),
                Token = jwtResult.Token,
                Expiration = jwtResult.Expiration,
                Role = user.Role.ToString(),
                IsVerification = user.IsVerification(),
            };
        }
    }
}
