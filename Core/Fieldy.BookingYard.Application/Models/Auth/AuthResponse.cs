namespace Fieldy.BookingYard.Application.Models.Auth
{
    public class AuthResponse
    {
        public required string UserID { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public required string Token { get; set; }
        public required DateTime Expiration { get; set; }
        public required string Email { get; set; }
        public required string Gender { get; set; } 
        public required string Role { get; set; }
        public required bool IsVerification { get; set; }
    }
}