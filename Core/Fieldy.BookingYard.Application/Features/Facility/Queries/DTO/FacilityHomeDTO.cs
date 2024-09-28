namespace Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;

public class FacilityHomeDTO
{
    public required Guid FacilityID { get; set; }
    public required string FacilityImage { get; set; }
    public required string FacilityName { get; set; }
    public required string FacilityAddress { get; set; }
    public required float FacilityRating { get; set; }
    public required decimal FacilityMinPrice { get; set; }
    public required decimal FacilityMaxPrice { get; set; }
    public required string StartTime { get; set; }
    public required string EndTime { get; set; }
}
