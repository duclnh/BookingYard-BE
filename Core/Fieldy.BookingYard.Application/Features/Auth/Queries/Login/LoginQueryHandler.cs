using Fieldy.BookingYard.Application.Common;
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
        private readonly ICommonService _commonService;

        public LoginQueryHandler(
            IUserRepository userRepository,
            IAppLogger<LoginQueryHandler> logger,
            ICommonService commonService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _commonService = commonService;
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
            if (!_commonService.Verify(request.Password, user.PasswordHash))
            {
                throw new BadRequestException("Password incorrect!");
            }
            _logger.LogInformation($"Login Account: {user.Email}");
            return _commonService.CreateTokenJWT(user);
        }
    }
}
