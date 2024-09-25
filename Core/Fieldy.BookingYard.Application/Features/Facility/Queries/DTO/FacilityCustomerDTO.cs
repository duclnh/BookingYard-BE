namespace Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;

public class FacilityCustomerDTO
{
   public required Guid FacilityID { get; set; }
   public required string FacilityImage { get; set; }
   public required string FacilityName { get; set; }
   public required string FacilityAddress { get; set; }
   public required float FacilityRating { get; set; }
   public required decimal FacilityMinPrice { get; set; }
   public required decimal FacilityMaxPrice { get; set; }
   public string? FacilityDistance { get; set; }
}
