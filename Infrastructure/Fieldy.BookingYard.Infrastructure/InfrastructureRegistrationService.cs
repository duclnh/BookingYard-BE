using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Abstractions.Hub;
using Fieldy.BookingYard.Application.Abstractions.Vnpay;
using Fieldy.BookingYard.Application.Models;
using Fieldy.BookingYard.Application.Models.Vnpay;
using Fieldy.BookingYard.Infrastructure.Email;
using Fieldy.BookingYard.Infrastructure.Hubs;
using Fieldy.BookingYard.Infrastructure.JWT;
using Fieldy.BookingYard.Infrastructure.LoggerAdaptor;
using Fieldy.BookingYard.Infrastructure.Utility;
using Fieldy.BookingYard.Infrastructure.Vnpay;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fieldy.BookingYard.Infrastructure
{
    public static class InfrastructureRegistrationService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient(typeof(IAppLogger<>), typeof(LoggerAdaptor<>));
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IJWTService, JWTService>();
            services.AddTransient<IUtilityService, UtilityService>();
            services.Configure<VnpayConfig>(configuration.GetSection("VNPAY"));
            services.AddTransient<IVnpayService, VnpayService>();
            services.AddTransient<INotificationService, NotificationService>();
            return services;
        }
    }
}


