using Fieldy.BookingYard.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("HistoryPoints")]
    public class HistoryPoint : EntityBase<int>
	{
        public Guid UserID { get; set; }
        public int Point { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}