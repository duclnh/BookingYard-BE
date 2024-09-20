using Fieldy.BookingYard.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Holidays")]
    public class Holiday : EntityBase<int>
    {
        public Guid FacilityID { get; set; }
        public Facility? Facility { get; set; }
        public DateTime Date { get; set; }
    }

}