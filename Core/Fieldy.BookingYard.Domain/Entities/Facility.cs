using System.ComponentModel.DataAnnotations.Schema;
using Fieldy.BookingYard.Domain.Abstractions;

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
        public ICollection<RegisterPackage>? RegisterPackages { get; set; }
        public ICollection<Court>? Courts { get; set; }


        public decimal GetMinPriceCourt()
        {
            var court = Courts?.OrderBy(x => x.CourtPrice).FirstOrDefault();

            return court == null ? 0 : court.CourtPrice;
        }
        public decimal GetMaxPriceCourt()
        {
            var court = Courts?.OrderByDescending(x => x.CourtPrice).FirstOrDefault();

            return court == null ? 0 : court.CourtPrice;
        }

        public string GetFacilityOpen()
        {
            Dictionary<string, (string name, int order)> dayTranslations = new Dictionary<string, (string name, int order)>
            {
                { "Monday", ("Thứ 2", 1) },
                { "Tuesday", ("Thứ 3", 2) },
                { "Wednesday", ("Thứ 4", 3) },
                { "Thursday", ("Thứ 5", 4) },
                { "Friday", ("Thứ 6", 5) },
                { "Saturday", ("Thứ 7", 6) },
                { "Sunday", ("Chủ Nhật", 7) }
            };



            // Sort the days based on the order defined in the dictionary
            List<int> openDayOrders = FacilityTimes.Select(day => dayTranslations[day.Time].order).OrderBy(order => order).ToList();

            List<string> result = new List<string>();

            // Check if all 7 days are selected (Monday to Sunday)
            if (openDayOrders.SequenceEqual(Enumerable.Range(1, 7)))
            {
                return "Thứ 2 đến Chủ Nhật";
            }

            // Group consecutive and non-consecutive days
            int start = 0;
            while (start < openDayOrders.Count)
            {
                int end = start;

                // Find the range of consecutive days
                while (end + 1 < openDayOrders.Count && openDayOrders[end + 1] == openDayOrders[end] + 1)
                {
                    end++;
                }

                // Handle ranges of consecutive days
                if (end > start)
                {
                    result.Add($"{dayTranslations.First(d => d.Value.order == openDayOrders[start]).Value.name} đến {dayTranslations.First(d => d.Value.order == openDayOrders[end]).Value.name}");
                }
                else
                {
                    // If it's not a range, just add the single day
                    result.Add(dayTranslations.First(d => d.Value.order == openDayOrders[start]).Value.name);
                }

                // Move to the next group
                start = end + 1;
            }

            // Join the results with comma and return
            return string.Join(", ", result);
        }
    }

}