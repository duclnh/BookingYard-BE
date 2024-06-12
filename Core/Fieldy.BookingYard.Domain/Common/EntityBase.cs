using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Common
{
    public abstract class EntityBase : IAuditEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string Name { get; set; }
        public DateTime? CreateDate { get; set; }

        [StringLength(32)]
        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(32)]
        public string? UpdateBy { get; set; }

        public DateTime? DeleteDate { get; set; }

    }
}