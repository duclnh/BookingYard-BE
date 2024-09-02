using Fieldy.BookingYard.Application.Account;
using Fieldy.BookingYard.Application.Contracts;
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
        private readonly IAccountService _accountService;

        public LoginQueryHandler(
            IUserRepository userRepository,
            IAppLogger<LoginQueryHandler> logger,
            IAccountService accountService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _accountService = accountService;
        }

        public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(x => (x.Phone == request.UserName 
                                                        || x.Email == request.UserName) 
                                                        && x.DeleteDate == null, null, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.UserName);
            }
            if (!_accountService.Verify(request.Password, user.PasswordHash))
            {
                throw new BadRequestException("Password incorrect!");
            }
            _logger.LogInformation($"Login Account: {user.Email}");
            return _accountService.CreateTokenJWT(user);
        }
    }
}
