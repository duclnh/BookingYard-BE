using Fieldy.BookingYard.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("PeakHours")]
    public class PeakHour : EntityBase<int>
    {
        public Guid FacilityID { get; set; }
        public Facility? Facility { get; set; }
		public TimeSpan Time { get; set; }
	}

}