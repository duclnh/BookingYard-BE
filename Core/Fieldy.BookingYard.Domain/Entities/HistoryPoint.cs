using Fieldy.BookingYard.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("HistoryPoints")]
    public class HistoryPoint : EntityBase<int>
	{
        public Guid UserID { get; set; }
        public User? User{ get; set; }
        public decimal Point { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Content { get; set; }
    }

}