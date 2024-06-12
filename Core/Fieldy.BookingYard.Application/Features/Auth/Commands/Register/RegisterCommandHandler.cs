using Fieldy.BookingYard.Application.Common;
using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models;
using Fieldy.BookingYard.Domain.Entities;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>

    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<RegisterCommandHandler> _logger;
        private readonly ICommonService _commonService;

        private readonly IEmailSender _emailSender;

        public RegisterCommandHandler(IUserRepository userRepository,
                                      IAppLogger<RegisterCommandHandler> logger,
                                      ICommonService commonService,
                                      IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _logger = logger;
            _commonService = commonService;
            _emailSender = emailSender;
        }
        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterCommandValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(request,cancellationToken);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Register User Request", validationResult);

            string generationCode = _commonService.GenerationCode();
            User user = new ()
            {
                Name = request.Name.Trim(),
                Email = request.Email.Trim(),
                Role = Domain.Enum.Role.Customer,
                PasswordHash = _commonService.Hash(request.Password),
                VerificationToken = _commonService.Hash(generationCode),
            };
            user.CreateBy = user.Id;
            user.UpdateBy = user.Id;

            await _userRepository.AddAsync(user);
            
            EmailMessage email = new ()
            {
                To = user.Email,
                Subject = "Verification Account",
                Body = generationCode,
            };
            var resultEmail = await _emailSender.SendEmailAsync(email);

            if(!resultEmail)
                throw new BadRequestException("Email invalid");
                
            _logger.LogInformation($"Create new account: {user.Email}");
            
            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ?"Create account successfully" : "Create account fail";
        }
    }
}