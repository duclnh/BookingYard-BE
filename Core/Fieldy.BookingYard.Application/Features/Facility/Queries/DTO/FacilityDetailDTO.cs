namespace Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;

public class FacilityDetailDTO
{
   public required Guid FacilityID { get; set; }
   public required string FacilityName { get; set; }
   public required List<string> FacilityImages { get; set; }
   public required List<string> Facility360s { get; set; }
   public required string FacilityAddress { get; set; }
   public required float FacilityRating { get; set; }
   public required IList<string> Sports { get; set; }
   public required string OpenDate { get; set; }
   public required string StartTime { get; set; }
   public required string EndTime { get; set; }
   public required int NumberFeedback { get; set; }
   public required string Description { get; set; }
   public required string Convenient { get; set; }
   public required decimal FacilityMinPrice { get; set; }
   public required decimal FacilityMaxPrice { get; set; }
   public required double Longitude { get; set; }
   public required double Latitude { get; set; }
   public int PercentFiveStar { get; set; }
   public int PercentFourStar { get; set; }
   public int PercentThreeStar { get; set; }
   public int PercentTwoStar { get; set; }
   public int PercentOneStar { get; set; }
}
