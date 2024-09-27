using Fieldy.BookingYard.Domain.Abstractions;
using Fieldy.BookingYard.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Feedbacks")]
    public class FeedBack : EntityBase<int>
    {
        public Guid UserID { get; set; }
        public User? User { get; set; }
        public Guid FacilityID { get; set; }
        public Facility? Facility { get; set; }
        public string? Content { get; set; }
        public int Rating { get; set; }
        public TypeFeedback TypeFeedback { get; set; }
        public bool IsShow { get; set; }
		public DateTime CreatedAt { get; set; }
        public ICollection<Image>? Images{ get; set; }
	}

}