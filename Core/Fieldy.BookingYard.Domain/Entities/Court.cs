using Fieldy.BookingYard.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Courts")]
    public class Court : EntityBase<int>
    {
        public required string CourtName { get; set; }   
        public required string Image {  get; set; }
        public required string Image360 { get; set; }
        public decimal CourtPrice { get; set;}
        public int NumberPlayer { get; set;}
        public int SportID { get; set;}
        public Sport? Sport { get; set;}
    }

}