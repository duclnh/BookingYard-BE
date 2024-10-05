using Fieldy.BookingYard.Domain.Abstractions;
using Fieldy.BookingYard.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Bookings")]
    public class Booking : EntityAuditBase<Guid>
	{
        public required string FullName { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
		public int CourtID { get; set; }
		public Court? Court { get; set; }
		public decimal CourtPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid UserID { get; set; }
        public User? User { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Guid? VoucherID { get; set; }
        public Voucher? Voucher { get; set; }
        public bool Status { get; set; }
        public bool PaymentStatus { get; set; }
        public string? PaymentCode { get; set; }
        public TypePayment PaymentMethod { get; set; }
    }

}