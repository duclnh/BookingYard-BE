namespace Fieldy.BookingYard.Application.Models.Auth
{
    public class AuthResponse
    {
        public required string UserID { get; set; }
        public required string Token { get; set; }
        public required DateTime Expiration { get; set; }
        public required string Role { get; set; }
        public required bool IsVerification { get; set; }
    }
}