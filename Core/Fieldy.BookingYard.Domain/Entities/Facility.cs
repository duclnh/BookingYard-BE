using System.ComponentModel.DataAnnotations.Schema;
using Fieldy.BookingYard.Domain.Common;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Facilities")]
    public class Facility : EntityAuditBase<Guid>
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Description { get; set; }
        public required string Convenient { get; set; }
        public required TimeOnly StartTime { get; set; }
        public required TimeOnly EndTime { get; set; }
        public required double Longitude { get; set; }
        public required double Latitude { get; set; }
        public required decimal MonthPrice { get; set; }
        public required decimal YeahPrice { get; set; }
        public required decimal HolidayPrice { get; set; }
        public required decimal PeakHourPrice { get; set; }
        public int WardID { get; set; }
        public int ProvinceID { get; set; }
        public required Guid UserID { get; set; }
        public required User User { get; set; }
    }

}