using Fieldy.BookingYard.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities;

[Table("Images")]
public class Image : EntityBase<int>
{
	public required string ImageLink { get; set; }
    public Guid? FacilityID { get; set; }
    public Facility? Facility { get; set; }
    public int? FeedbackID { get; set; }
    public FeedBack? FeedBack { get; set; }
    public int? ReportID { get; set; }
    public Report? Report { get; set; } 
}
