using System.ComponentModel.DataAnnotations.Schema;
using Fieldy.BookingYard.Domain.Common;
using Microsoft.VisualBasic;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Facilities")]
    public class Facility : EntityAuditBase<Guid>
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string FullAddress { get; set; }
        public required string Description { get; set; }
        public required string Convenient { get; set; }
        public required TimeSpan StartTime { get; set; }
        public required TimeSpan EndTime { get; set; }
        public required double Longitude { get; set; }
        public required double Latitude { get; set; }
        public required decimal MonthPrice { get; set; }
        public required decimal YearPrice { get; set; }
        public required decimal HolidayPrice { get; set; }
        public required decimal PeakHourPrice { get; set; }
        public required string Image { get; set; }
        public string? Logo { get; set; }
        public int WardID { get; set; }
        public int DistrictID { get; set; }
        public int ProvinceID { get; set; }
        public required Guid UserID { get; set; }
        public User? User { get; set; }
        public bool IsActive { get; set; }
        public ICollection<FeedBack>? FeedBacks { get; set; }
        public ICollection<Image>? Images { get; set; }
        public ICollection<FacilityTime>? FacilityTimes { get; set; }
        public ICollection<Holiday>? Holidays { get; set; }
        public ICollection<PeakHour>? PeakHours { get; set; }

    }

}