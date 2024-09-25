namespace Fieldy.BookingYard.Application.Features.Court;

public class CourtDTO
{
    public int CourtID { get; set; }
    public required string CourtName { get; set; }
    public required string Image { get; set; }
    public required string SportID { get; set; }
    public required string SportName { get; set; }
    public int NumberPlayer { get; set; }
    public required decimal CourtPrice { get; set; }
    public bool IsActive { get; set; }
}
