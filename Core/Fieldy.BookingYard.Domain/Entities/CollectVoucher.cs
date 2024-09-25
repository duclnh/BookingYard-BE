using Fieldy.BookingYard.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("CollectVouchers")]
    public class CollectVoucher : EntityBase<Guid>
    {
        public Guid UserID { get; set; }
        public User? User { get; set; }
        public Guid VoucherID { get; set; }
        public Voucher? Voucher { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}