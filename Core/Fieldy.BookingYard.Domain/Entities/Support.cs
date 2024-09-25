using System.ComponentModel.DataAnnotations.Schema;
using Fieldy.BookingYard.Domain.Abstractions;
using Fieldy.BookingYard.Domain.Enums;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Supports")]
    public class Support : EntityBase<int>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Content { get; set; }
        public string? Note { get; set; }
        public required TypeSupport TypeSupport { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsProcessed() => ModifiedBy != null;
    }
}