using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fieldy.BookingYard.Domain.Enum;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Accounts")]
    public class Account : AuditableEntity
    {
        [MaxLength(30)]
        public string? GoogleID { get; set; }

        [MaxLength(100)]
        public string? PasswordHash { get; set; }

        [MaxLength(100)]
        public string? VerificationToken { get; set; }

        [MaxLength(100)]
        public string? ResetToken { get; set; }
        public DateTime? ExpirationResetToken { get; set; }

        [Column(TypeName = "smallint")]
        public Role Role { get; set; }
        public bool IsBan { get; set; }

        public bool IsVerification() => VerificationToken == null;

    }

}