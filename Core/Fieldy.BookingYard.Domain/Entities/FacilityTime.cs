using Fieldy.BookingYard.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("FacilityTimes")]
    public class FacilityTime : EntityBase<int>
    {
        public Guid FacilityID { get; set; }
        public Facility? Facility { get; set; }
        public required string Time { get; set; }
    }

}