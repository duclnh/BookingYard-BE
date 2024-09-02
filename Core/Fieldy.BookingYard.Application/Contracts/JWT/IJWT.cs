using Fieldy.BookingYard.Application.Models.Auth;

namespace Fieldy.BookingYard.Application.Contracts.JWT
{
    public interface IJWTService
    {
        public string CreateTokenJWT();
        public string AccountID { get; }
    };
}