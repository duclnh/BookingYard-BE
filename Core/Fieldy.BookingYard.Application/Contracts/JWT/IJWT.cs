using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Application.Contracts.JWT
{
    public interface IJWTService
    {
        public string CreateTokenJWT(User user);
        public Guid UserID { get; }
    };
}