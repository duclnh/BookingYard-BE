using System.Net;
using System.Net.Mail;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Models;
using Microsoft.Extensions.Options;

namespace Fieldy.BookingYard.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        public EmailSettings _emailSettings { get; }

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendEmailAsync(EmailMessage emailContent)
        {
            try
            {
                MailMessage mailMessage = new MailMessage()
                {
                    Subject = emailContent.Subject,
                    Body = emailContent.Body,
                    IsBodyHtml = true,
                };
                mailMessage.From = new MailAddress(
                   _emailSettings.FromEmailAddress,
                   _emailSettings.FromDisplayName
                );
                mailMessage.To.Add(emailContent.To);
                var smtp = new SmtpClient()
                {
                    EnableSsl =_emailSettings.Smtp.EnableSsl,
                    Host =_emailSettings.Smtp.Host,
                    Port =_emailSettings.Smtp.Port,
                };
                var network = new NetworkCredential(
                   _emailSettings.Smtp.EmailAddress,
                   _emailSettings.Smtp.ApiKey
                );
                smtp.Credentials = network;
                await smtp.SendMailAsync(mailMessage);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error SendMail: {0}", ex);
                return false;
            }
        }
    }
}