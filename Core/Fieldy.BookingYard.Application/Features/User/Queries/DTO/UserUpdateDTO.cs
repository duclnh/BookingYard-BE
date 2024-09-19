namespace Fieldy.BookingYard.Application.Models.User
{
    public class UserUpdateDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public required string Role { get; set; }
    }
}