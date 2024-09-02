using Fieldy.BookingYard.Application.Account;
using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.Commands.SendVerificationCode
{
    public class SendVerificationCodeCommandHandler : IRequestHandler<SendVerificationCodeCommand, string>
    {

        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<SendVerificationCodeCommandHandler> _logger;
        private readonly IAccountService _accountService;

        private readonly IEmailSender _emailSender;

        public SendVerificationCodeCommandHandler(IUserRepository userRepository,
                                      IAppLogger<SendVerificationCodeCommandHandler> logger,
                                      IAccountService accountService,
                                      IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _logger = logger;
            _accountService = accountService;
            _emailSender = emailSender;
        }
        public async Task<string> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(x => x.Id == request.UserID && x.DeleteDate == null, null, cancellationToken);
            
            if (user == null)
                throw new NotFoundException(nameof(user), request.UserID);

            var newVerificationCode = _accountService.GenerationCode();
            user.VerificationToken = _accountService.Hash(newVerificationCode);
            _userRepository.Update(user);

            EmailMessage email = new ()
            {
                To = user.Email,
                Subject = "Verification Account",
                Body = newVerificationCode,
            };
            var resultEmail = await _emailSender.SendEmailAsync(email);

            if (!resultEmail)
                throw new BadRequestException("Email invalid");

            _logger.LogInformation($"{user.Email} call take new verification code");
            return  await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Send verification code successfully" : "Send verification code fail";
        }
    }
}