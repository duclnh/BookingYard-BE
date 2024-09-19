namespace Fieldy.BookingYard.Application.Features.Facility.Queries;

public class FacilityAdminDTO {
    public required Guid FacilityID;
    public required string OwnerName;
    public required string Image;
    public required string FacilityName;
    public required string Address;
    public required bool IsActive;
}