using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Fieldy.BookingYard.Application.Features.Court.Commands.CreateCourt;

public class CreateCourtCommand : IRequest<string>
{
    public Guid FacilityID { get; set; }
    public required string CourtName { get; set; }
    public required IFormFile Image { get; set; }
    public required IFormFile Image360 { get; set; }
    public int NumberPlayer { get; set; }
    public int SportID { get; set; }
    public decimal Price { get; set; }
}
