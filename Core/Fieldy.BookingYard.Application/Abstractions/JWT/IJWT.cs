using Fieldy.BookingYard.Application.Models.Jwt;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.Abstractions
{
    public interface IJWTService
    {
        public JWTResponse CreateTokenJWT(User user);
        public Guid UserID { get; }
    };
}