using AutoMapper;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models.Auth;
using Fieldy.BookingYard.Application.Models.Jwt;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.Google
{
    public class GoogleCommandHandler : IRequestHandler<GoogleCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jwtService;
        private readonly IMapper _mapper;
        public GoogleCommandHandler(IUserRepository userRepository,
                                    IJWTService jwtService,
                                    IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(GoogleCommand request, CancellationToken cancellationToken)
        {
            //validation user
            var validator = new GoogleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid register user by Google", validationResult);

            //check email in database if have you user response AuthResponse else create new user
            var userExist = await _userRepository.Find(x => x.Email == request.Email && x.IsDeleted == false, cancellationToken);

            JWTResponse jwtResult;

            if (userExist != null)
            {
                if (!string.IsNullOrEmpty(userExist.UserName))
                    throw new BadRequestException("This email is already registered.");

                if (userExist.IsBanned)
                    throw new BadRequestException("User is banned!");

                jwtResult = _jwtService.CreateTokenJWT(userExist);

                return new AuthResponse()
                {
                    UserID = userExist.Id.ToString(),
                    Token = jwtResult.Token,
                    Expiration = jwtResult.Expiration,
                    Role = userExist.Role.ToString(),
                    IsVerification = userExist.IsVerification(),
                };
            }

            //map data GoogleCommand -> User
            var userCreate = _mapper.Map<Domain.Entities.User>(request);
            if (userCreate == null)
                throw new BadRequestException("Error system register user by Google");

            userCreate.PasswordHash = string.Empty;
            //add role customer to user    
            userCreate.Role = Role.Customer;

            //add gender other to user
            userCreate.Gender = Gender.Other;

            //create new user
            await _userRepository.AddAsync(userCreate);

            //save to database
            var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (result < 1)
                throw new BadRequestException("Error system register user by Google");

            //get user
            var user = await _userRepository.Find(x => x.GoogleID == request.GoogleID);
            if (user == null)
                throw new NotFoundException(nameof(user), request.GoogleID);

            if (user.IsBanned)
                throw new BadRequestException("User is banned!");

            jwtResult = _jwtService.CreateTokenJWT(user);

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
