using System.Text;
using Fieldy.BookingYard.Application.Exceptions;
using Fieldy.BookingYard.Application.Models.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Fiedly.BookingYard.Api.Configurations
{
    public static class AuthenticationSecurityConfiguration
    {
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           }).AddJwtBearer(o =>
           {
               o.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero,
                   ValidIssuer = configuration["JwtSettings:Issuer"],
                   ValidAudience = configuration["JwtSettings:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"] ?? throw new BadRequestException("Key secret not null")))
               };
           });

            return services;
        }
    }
}