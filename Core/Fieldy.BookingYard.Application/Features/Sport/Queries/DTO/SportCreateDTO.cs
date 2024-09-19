using System;

namespace Fieldy.BookingYard.Application.Features.Sport.Queries.DTO;

public class SportCreateDTO
{
    public int SportID { get; set; }
    public required string SportName { get; set; }
}
