using System.Security.Claims;
using Fieldy.BookingYard.Application.Contracts.JWT;
using Fieldy.BookingYard.Application.Models.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Fieldy.BookingYard.Infrastructure.JWT
{
    public class JWTService : IJWTService
    {
        private readonly JwtSettings _jwtSetting;
        private readonly IHttpContextAccessor _contextAccessor;
        public JWTService(IOptions<JwtSettings> jwtSettings, IHttpContextAccessor contextAccessor)
        {
            _jwtSetting = jwtSettings.Value;
            _contextAccessor = contextAccessor;
        }
        public string AccountID => _contextAccessor?.HttpContext?.User.FindFirstValue("AccountID") ?? "Fieldy";
        public string CreateTokenJWT()
        {
            throw new NotImplementedException();
        }
    }
}