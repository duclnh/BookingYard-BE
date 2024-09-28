using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models;
using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.SendVerificationCode
{
    public class SendVerificationCodeCommandHandler : IRequestHandler<SendVerificationCodeCommand, string>
    {

        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<SendVerificationCodeCommandHandler> _logger;
        private readonly IUtilityService _utility;

        private readonly IEmailSender _emailSender;

        public SendVerificationCodeCommandHandler(IUserRepository userRepository,
                                      IAppLogger<SendVerificationCodeCommandHandler> logger,
                                      IUtilityService utility,
                                      IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _logger = logger;
            _utility = utility;
            _emailSender = emailSender;
        }
        public async Task<string> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => x.Id == request.UserID && x.IsDeleted == false, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.UserID);

            var newVerificationCode = _utility.GenerationCode();
            user.VerificationToken = _utility.Hash(newVerificationCode);
            _userRepository.Update(user);

            EmailMessage email = new()
            {
                To = user.Email,
                Subject = "Xác nhận tài khoản",
                Body = _emailSender.GetVerificationEmail(newVerificationCode),
            };
            var resultEmail = await _emailSender.SendEmailAsync(email);

            if (!resultEmail)
                throw new BadRequestException("Email invalid");

            _logger.LogInformation($"{user.Email} call take new verification code");

            var result = await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (result < 0)
                throw new BadRequestException("Send verification code fail");
                
            return "Send verification code successfully";
        }
    }
}