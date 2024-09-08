namespace Fieldy.BookingYard.Application.Features.FacilityTime.Queries
{
	public class FacilityTimeDto
	{
		public int FacilityTimeID { get; set; }
		public Guid FacilityID { get; set; }
		public TimeSpan Time { get; set; }
	}
}
