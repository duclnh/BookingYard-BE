using Fieldy.BookingYard.Application.Models;

namespace Fieldy.BookingYard.Application.Abstractions
{
    public interface IEmailSender{
        Task<bool> SendEmailAsync(EmailMessage email);
    };
}