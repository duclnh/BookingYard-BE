using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Facilitys")]
    public class Facility
    {
        public Guid FacilityID { get; set; }
        public string? FacilityName { get; set; }
    }

}