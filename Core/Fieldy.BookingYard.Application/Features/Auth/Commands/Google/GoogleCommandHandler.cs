using AutoMapper;
using Fieldy.BookingYard.Application.Account;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models.Auth;
using Fieldy.BookingYard.Domain.Entities;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.Google
{
    public class GoogleCommandHandler : IRequestHandler<GoogleCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public GoogleCommandHandler(IUserRepository userRepository,
                                    IAccountService accountService,
                                    IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<AuthResponse> Handle(GoogleCommand request, CancellationToken cancellationToken)
        {
            //validation user
            var validator = new GoogleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid register user by Google", validationResult);

            //check email in database if have you user response AuthResponse else create new user
            var userExist = await _userRepository.Get(x => x.Email == request.Email);
            if (userExist != null)
            {
                //check GoogleID exist in database if exist return AuthResponse else update GoogleID to user
                if (userExist.GoogleID != null)
                    return _accountService.CreateTokenJWT(userExist);

                userExist.GoogleID = request.GoogleID;
                userExist.ImageUrl ??= request.ImageUrl;
                _userRepository.Update(userExist);
            }
            else
            {
                //map data GoogleCommand -> User
                var userCreate = _mapper.Map<User>(request);
                if (userCreate == null)
                    throw new BadRequestException("Error system register user by Google");

                //add role customer to user    
                userCreate.Role = Domain.Enum.Role.Customer;

                //create new user
                await _userRepository.AddAsync(userCreate);
            }

            //save to database
            var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (result < 1)
                throw new BadRequestException("Error system register user by Google");

            //get user
            var user = await _userRepository.Get(x => x.GoogleID == request.GoogleID);
            if (user == null)
                throw new NotFoundException(nameof(user), request.GoogleID);

            return _accountService.CreateTokenJWT(user);
        }
    }
}
