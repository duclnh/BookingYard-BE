namespace Fieldy.BookingYard.Application.Features.Holiday.Queries
{
	public class HolidayDto
	{
		public Guid HolidayID { get; set;}
		public Guid FacilityID { get; set; }
		public DateOnly Date { get; set; }
	}
}
