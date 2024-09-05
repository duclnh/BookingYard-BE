namespace Fieldy.BookingYard.Application.Features.User.Queries
{
    public class UserDTO
    {
        public required string UserID { get; set; }
        public required string Name { get; set; }

        public required string Address { get; set; }
        public required string Email { get; set; }

        public required string Phone { get; set; }
        public required string Gender { get; set; }
        public int Point { get; set; }

        public required string Role { get; set; }

    }
}