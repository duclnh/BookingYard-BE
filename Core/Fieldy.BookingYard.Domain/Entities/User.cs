using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fieldy.BookingYard.Domain.Common;
using Fieldy.BookingYard.Domain.Enum;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Users")]
    public class User : Entity
    {
        [MaxLength(30)]
        public required string Name { get; set; }

        [MaxLength(255)]
        public  string? Address { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public required string Email { get; set; }

        [StringLength(12, MinimumLength = 10)]
        public  string? Phone { get; set; }
        public bool? Gender { get; set; }
        [MaxLength(150)]
        public string? ImageUrl { get; set; }
        [MaxLength(30)]
        public string? GoogleID { get; set; }
        public int Point { get; set; } = 0;

        [MaxLength(100)]
        public string? PasswordHash { get; set; }

        [MaxLength(100)]
        public string? VerificationToken { get; set; }

        [MaxLength(100)]
        public string? ResetToken { get; set; }
        public DateTime? ExpirationResetToken { get; set; }

        [Column(TypeName = "smallint")]
        public Role Role { get; set; }

        public bool IsVerification() => VerificationToken == null;

    }

}