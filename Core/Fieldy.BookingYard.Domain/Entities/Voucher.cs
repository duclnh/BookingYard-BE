using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fieldy.BookingYard.Domain.Abstractions;

namespace Fieldy.BookingYard.Domain.Entities
{
	[Table("Vouchers")]
	public class Voucher : EntityAuditBase<Guid>
	{
		public required string VoucherName { get; set; }
		public string? Image { get; set; }
		[MinLength(0), MaxLength(100)]
		public int Percentage { get; set; }
		public string? Code { get; set; }
		public string? VoucherDescription { get; set; }
		[Column("StartDate")]
		public DateTime RegisterDate { get; set; }
		[Column("EndDate")]
		public DateTime ExpiredDate { get; set; }
		public string? Reason { get; set; }
		public int Quantity { get; set; }
		public bool Status { get; set; }
		public Guid? FacilityID { get; set; }
		public Facility? Facility { get; set; }
		public int Quantity { get; set; }
		public int? SportID { get; set; }
		public Sport? Sport { get; set; }
		public ICollection<CollectVoucher>? CollectVouchers { get; set; }
		public ICollection<Booking>? Bookings { get; set; }
	}
}
