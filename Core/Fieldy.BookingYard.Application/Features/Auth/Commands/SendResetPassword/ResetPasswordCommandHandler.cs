using Fieldy.BookingYard.Application.Common;
using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models;
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Auth.SendResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<ResetPasswordCommandHandler> _logger;
        private readonly ICommonService _commonService;

        private readonly IEmailSender _emailSender;

        public ResetPasswordCommandHandler(IUserRepository userRepository,
                                      IAppLogger<ResetPasswordCommandHandler> logger,
                                      ICommonService commonService,
                                      IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _logger = logger;
            _commonService = commonService;
            _emailSender = emailSender;
        }
        public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(x => x.Email == request.Email && x.DeleteDate == null, null, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(user), request.Email);

            _logger.LogInformation($"{user.Email} call reset password");

            var resetToken = _commonService.GenerationCode();
            user.ResetToken = _commonService.Hash(resetToken);
            user.ExpirationResetToken = DateTime.Now.AddMinutes(15);
            _userRepository.Update(user);

            EmailMessage email = new()
            {
                To = user.Email,
                Subject = "Reset Password Account",
                Body = resetToken,
            };
            var resultEmail = await _emailSender.SendEmailAsync(email);

            if (!resultEmail)
                throw new BadRequestException("Email invalid");

            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Send reset password code successfully" : "Send reset password code fail";
        }
    }
}