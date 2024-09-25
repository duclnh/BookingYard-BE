namespace Fieldy.BookingYard.Application.Features.Facility.Queries;

public class FacilityAdminDTO {
    public required Guid FacilityID{ get; set; }
    public required string OwnerName{ get; set; }
    public required string Image{ get; set; }
    public required string FacilityName{ get; set; }
    public required string Address{ get; set; }
    public required bool IsActive{ get; set; }
}