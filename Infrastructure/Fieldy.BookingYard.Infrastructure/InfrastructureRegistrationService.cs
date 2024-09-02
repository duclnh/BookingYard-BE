using Fieldy.BookingYard.Application.Contracts;
using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Application.Models;
using Fieldy.BookingYard.Infrastructure.Email;
using Fieldy.BookingYard.Infrastructure.LoggerAdaptor;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fieldy.BookingYard.Infrastructure
{
    public static class InfrastructureRegistrationService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient(typeof(IAppLogger<>), typeof(LoggerAdaptor<>));
            services.AddTransient<IHttpContextAccessor , HttpContextAccessor>();
            services.AddTransient<IJWTService, IJWTService>();
            return services;
        }
    }
}


