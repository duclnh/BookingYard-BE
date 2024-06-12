using Fieldy.BookingYard.Application.Models;

namespace Fieldy.BookingYard.Application.Contracts
{
    public interface IEmailSender{
        Task<bool> SendEmailAsync(EmailMessage email);
    };
}