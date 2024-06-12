using Fieldy.BookingYard.Domain.Enum;

namespace Fieldy.BookingYard.Application.Models.Auth
{
    public class AuthResponse
    {

        public required string UserID { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }

        public required string Token { get; set; }

        public required string Email { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public required string Role { get; set; }
        public required bool IsVerification { get; set; }
    }
}