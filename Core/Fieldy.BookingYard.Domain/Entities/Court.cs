using Fieldy.BookingYard.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Courts")]
    public class Court : EntityBase<int>
    {
        public Guid FacilityID { get; set; }
        public Facility? Facility { get; set; }
        public required string CourtName { get; set; }
        public required string Image { get; set; }
        public required string Image360 { get; set; }
        public decimal CourtPrice { get; set; }
        public int NumberPlayer { get; set; }
        public int SportID { get; set; }
        public Sport? Sport { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }

}