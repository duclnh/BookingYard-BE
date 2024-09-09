namespace Fieldy.BookingYard.Application.Features.PeakHour.Queries
{
	public class PeakHourDto
	{
		public int PeakHourID { get; set; }
		public Guid FacilityID { get; set; }
		public TimeSpan Time { get; set; }

	}
}
