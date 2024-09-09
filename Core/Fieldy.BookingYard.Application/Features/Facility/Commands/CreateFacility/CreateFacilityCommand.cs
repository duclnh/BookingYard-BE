
using MediatR;

namespace Fieldy.BookingYard.Application.Features.Facility.Commands.CreateFacility;

public class CreateFacilityCommand : IRequest<string>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string FacilityName { get; set; }
    public required string Address { get; set; }
    public required string Description { get; set; }
    public required string Convenient  { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
    public required double Longitude { get; set; }
    public required double Latitude { get; set; }
    public required decimal MonthPrice { get; set; }
    public required decimal YeahPrice { get; set; }
    public required decimal HolidayPrice { get; set; }
    public required decimal PeakHourPrice { get; set; }
    public List<string> OpenDate { get; set; } = new List<string>();
    public List<DateTime> HolidayDate { get; set; } = new List<DateTime>();
    public List<TimeSpan> PeakHour { get; set; } = new List<TimeSpan>();
    public int WardID { get; set; }
    public int ProvinceID {get; set; }
}
