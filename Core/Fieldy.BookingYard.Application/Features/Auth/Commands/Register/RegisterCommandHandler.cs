using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Enums;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>

    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<RegisterCommandHandler> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUtilityService _utility;

        public RegisterCommandHandler(IUserRepository userRepository,
                                      IAppLogger<RegisterCommandHandler> logger,
                                      IUtilityService utility,
                                      IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _logger = logger;
            _emailSender = emailSender;
            _utility = utility;
        }
        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterCommandValidator(_userRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Register User Request", validationResult);

            string generationCode = _utility.GenerationCode();
            Domain.Entities.User user = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                Email = request.Email.Trim(),
                Gender = request.gender,
                Role = Role.Customer,
                PasswordHash = _utility.Hash(request.Password),
                VerificationToken = _utility.Hash(generationCode),
            };

            await _userRepository.AddAsync(user);

            EmailMessage email = new()
            {
                To = user.Email,
                Subject = "Xác nhận tài khoản",
                Body = _emailSender.GetVerificationEmail(generationCode),
            };
            var resultEmail = await _emailSender.SendEmailAsync(email);

            if (!resultEmail)
                throw new BadRequestException("Email invalid");

            _logger.LogInformation($"Create new account: {user.Email}");

            var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (result < 0)
                throw new BadRequestException("Create new user fail!");

            return "Create new user successfully";
        }
    }
}