using Fieldy.BookingYard.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
	[Table("Discounts")]
	public class Discount : EntityBase<Guid>
	{
		public required string DiscountName { get; set; }
		public string? Image { get; set; }
		[MinLength(0), MaxLength(100)]
		public int Percentage { get; set; }
		public string? DiscountDescription { get; set; }
		[Column("StartDate")]
		public DateTime RegisterDate { get; set; }
		[Column("EndDate")]
		public DateTime ExpiredDate { get; set; }
		public string? Reason { get; set; }
		public bool Status { get; set; }
		public Guid FacilityID { get; set; }
		public Facility Facility { get; set; }
		public Guid CategorySportID { get; set; }
		//public Category Category { get; set; }
	}
}
