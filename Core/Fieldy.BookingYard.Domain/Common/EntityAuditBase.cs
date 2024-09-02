using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Common
{
    public abstract class EntityAuditBase<TKey> : EntityBase<TKey>, IAuditable
    {
        public Guid CreateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UpdateBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDelete { get; set; }
    }
}