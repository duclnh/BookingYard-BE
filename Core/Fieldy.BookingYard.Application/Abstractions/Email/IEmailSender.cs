using Fieldy.BookingYard.Application.Models;

namespace Fieldy.BookingYard.Application.Abstractions
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(EmailMessage email);
        public string GetTextRegisterFacility(string account, string password);
        public string GetResetPasswordEmail(string verificationCode);
        public string GetVerificationEmail(string verificationCode);
    };
}