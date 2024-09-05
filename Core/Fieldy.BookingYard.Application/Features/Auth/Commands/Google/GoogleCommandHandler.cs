using AutoMapper;
using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models.Auth;
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
            var userExist = await _userRepository.Find(x => x.Email == request.Email && x.IsBanned == false && x.IsDeleted == false, cancellationToken);
            if (userExist != null)
            {
                //check GoogleID exist in database if exist return AuthResponse else update GoogleID to user
                if (userExist.GoogleID != null)
                    return new AuthResponse()
                    {
                        UserID = userExist.Id.ToString(),
                        ImageUrl = userExist.ImageUrl,
                        Name = userExist.Name,
                        Token = _jwtService.CreateTokenJWT(userExist),
                        Email = userExist.Email,
                        Gender = userExist.Gender.ToString(),
                        Role = userExist.Role.ToString(),
                        IsVerification = userExist.IsVerification(),
                    };

                userExist.GoogleID = request.GoogleID;
                userExist.ImageUrl ??= request.ImageUrl;
                _userRepository.Update(userExist);
            }
            else
            {
                //map data GoogleCommand -> User
                var userCreate = _mapper.Map<Domain.Entities.User>(request);
                if (userCreate == null)
                    throw new BadRequestException("Error system register user by Google");

                //add role customer to user    
                userCreate.Role = Domain.Enum.Role.Customer;

                //add gender other to user
                userCreate.Gender = Domain.Enum.Gender.Other;

                //create new user
                await _userRepository.AddAsync(userCreate);
            }

            //save to database
            var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (result < 1)
                throw new BadRequestException("Error system register user by Google");

            //get user
            var user = await _userRepository.Find(x => x.GoogleID == request.GoogleID);
            if (user == null)
                throw new NotFoundException(nameof(user), request.GoogleID);

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
