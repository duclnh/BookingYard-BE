using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fieldy.BookingYard.Domain.Abstractions;
using Fieldy.BookingYard.Domain.Enums;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Users")]
    public class User : EntityAuditBase<Guid>
    {
        [MinLength(5)]
        public string? UserName { get; set; }
        [MaxLength(30)]
        public required string Name { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public required string Email { get; set; }

        [StringLength(12, MinimumLength = 10)]
        public string? Phone { get; set; }
        public Gender Gender { get; set; }

        public string? ImageUrl { get; set; }
        [MaxLength(30)]
        public string? GoogleID { get; set; }
        public int Point { get; set; } = 0;

        [MaxLength(100)]
        public required string PasswordHash { get; set; }

        [MaxLength(100)]
        public string? VerificationToken { get; set; }
        public DateTime? ExpirationVerificationToken { get; set; }

        [MaxLength(100)]
        public string? ResetToken { get; set; }
        public DateTime? ExpirationResetToken { get; set; }
        public bool IsBanned { get; set; }

        [Column(TypeName = "smallint")]
        public Role Role { get; set; }
        public int? WardID { get; set; }

        public bool IsVerification() => VerificationToken == null;
        public ICollection<FeedBack> FeedBacks { get; set; }
        public ICollection<HistoryPoint> HistoryPoints { get; set; }

    }

}