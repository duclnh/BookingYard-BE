using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fieldy.BookingYard.Application.Common;
using Fieldy.BookingYard.Application.Models.Auth;
using Fieldy.BookingYard.Application.Models.Jwt;
using Fieldy.BookingYard.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Fieldy.BookingYard.Infrastructure.Common
{
    public class CommonService : ICommonService
    {
        private readonly JwtSettings _jwtSetting;
        private readonly IHttpContextAccessor _contextAccessor;

        public CommonService(IOptions<JwtSettings> jwtSettings, IHttpContextAccessor contextAccessor)
        {
            _jwtSetting = jwtSettings.Value;
            _contextAccessor = contextAccessor;
        }
        public string? UserId => _contextAccessor?.HttpContext?.User.FindFirstValue("UserID");

        public AuthResponse CreateTokenJWT(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSetting.Key);
            var securityKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]{
                    new Claim("UserID", user.Id),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Verification", user.IsVerification().ToString()),
            };
            var timeExpiration = DateTime.Now.AddMinutes(_jwtSetting.DurationMinutes);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: timeExpiration,
                signingCredentials: signingCredentials
            );
            return new AuthResponse()
            {
                UserID = user.Id,
                Name = user.Name,
                ImageUrl = user.ImageUrl,
                Token = tokenHandler.WriteToken(jwtSecurityToken),
                Email = user.Email,
                ExpirationTime = timeExpiration,
                Role = user.Role.ToString(),
                IsVerification = user.IsVerification(),
            };
        }

        public string GenerationCode()
        {
            const string chars = "0123456789";
            Random random = new();
            string randomCode = "";
            for (int i = 0; i < 6; i++)
            {
                randomCode += chars[random.Next(0, chars.Length - 1)];
            }

            return randomCode;
        }

        public string Hash(string content)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(content, 10);
        }

        public bool Verify(string content, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(content, hash);
        }
    }
}