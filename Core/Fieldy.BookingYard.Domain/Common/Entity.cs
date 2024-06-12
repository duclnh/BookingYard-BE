using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Common
{
    public class Entity : IAuditEntity
    {
        public Entity()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        [Key]
        [StringLength(32)]
        public string Id { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(32)]
        public string? CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(32)]
        public string? UpdateBy { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}


