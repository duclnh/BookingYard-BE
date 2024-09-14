
using MediatR;
using Microsoft.AspNetCore.Http;
namespace Fieldy.BookingYard.Application.Features.Facility.Commands.CreateFacility;

public class CreateFacilityCommand : IRequest<string>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public required string Description { get; set; }
    public required string Convenient { get; set; }
    public required double Longitude { get; set; }
    public required double Latitude { get; set; }
    public required string FacilityName { get; set; }
    public required TimeSpan StartTime { get; set; }
    public required TimeSpan EndTime { get; set; }
    public required decimal MonthPrice { get; set; }
    public required decimal YearPrice { get; set; }
    public required decimal HolidayPrice { get; set; }
    public required decimal PeakHourPrice { get; set; }
    public List<string> OpenDate { get; set; } = new List<string>();
    public List<DateTime>? HolidayDate { get; set; }
    public List<TimeSpan>? PeakHour { get; set; }
    public required IFormFile Image { get; set; }
    public IFormFile? Logo { get; set; }
    public List<IFormFile>? Other { get; set; }
    public int WardID { get; set; }
    public int DistrictID { get; set; }
    public int ProvinceID { get; set; }
}
