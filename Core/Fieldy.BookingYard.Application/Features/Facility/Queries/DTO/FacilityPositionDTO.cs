using System;

namespace Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;

public class FacilityPositionDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
