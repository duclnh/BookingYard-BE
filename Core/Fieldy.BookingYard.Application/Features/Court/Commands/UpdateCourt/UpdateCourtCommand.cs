using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Fieldy.BookingYard.Application.Features.Court.Commands.UpdateCourt;

public class UpdateCourtCommand : IRequest<string>
{
    public required int CourtID { get; set; }
    public required Guid FacilityID { get; set; }
    public string? CourtName { get; set; }
    public IFormFile? Image { get; set; }
    public IFormFile? Image360 { get; set; }
    public int? NumberPlayer { get; set; }
    public int? SportID { get; set; }
    public decimal? CourtPrice { get; set; }
    public bool? IsActive { get; set;}  
}
