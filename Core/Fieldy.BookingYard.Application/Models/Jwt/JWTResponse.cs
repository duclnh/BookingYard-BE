namespace Fieldy.BookingYard.Application.Models.Jwt;

public class JWTResponse
{
    public required string Token { get; set; } 
    public required DateTime Expiration { get; set; }
}
