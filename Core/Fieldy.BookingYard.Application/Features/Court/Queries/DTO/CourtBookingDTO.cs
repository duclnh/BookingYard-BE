using System;

namespace Fieldy.BookingYard.Application.Features.Court.Queries.DTO;

public class CourtBookingDTO    
{
    public int CourtID { get; set; }
    public required string CourtName { get; set;}
    public required string Image { get; set; }
    public required string Image360 { get; set; }
    public int NumberPlayer { get; set; }
    public decimal CourtPrice { get; set; }
}
