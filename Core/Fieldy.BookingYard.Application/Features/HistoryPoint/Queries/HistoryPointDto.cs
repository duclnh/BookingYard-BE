namespace Fieldy.BookingYard.Application.Features.HistoryPoint.Queries
{
	public class HistoryPointDto
	{
		public int HistoryPointID { get; set; }
		public int Point { get; set; }
		public string? Content { get; set; }
		public required string CreatedAt { get; set; }
	}
}
