using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fieldy.BookingYard.Application.Abstractions;
using Fieldy.BookingYard.Application.Models.Jwt;
using Fieldy.BookingYard.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
        public Guid UserID
        {
            get
            {
                var userId = _contextAccessor?.HttpContext?.User.FindFirstValue("UserId");
                return Guid.TryParse(userId, out var result) ? result : Guid.Empty;
            }
        }
        public JWTResponse CreateTokenJWT(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSetting.Key);
            var securityKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]{
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var timeExpiration = DateTime.Now.AddMinutes(_jwtSetting.DurationMinutes);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: timeExpiration,
                signingCredentials: signingCredentials
            );

            return new JWTResponse
            {
                Token = tokenHandler.WriteToken(jwtSecurityToken),
                Expiration = timeExpiration,
            };
        }
    }
}