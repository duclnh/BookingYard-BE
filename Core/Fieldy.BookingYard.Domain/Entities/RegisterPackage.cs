using Fieldy.BookingYard.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("RegisterPackages")]
    public class RegisterPackage : EntityBase<Guid>, IDateTracking
    {
        public Guid PackageID { get; set; }
        public Package? Package { get; set; }
        public Guid UserID { get; set; }
        [Column("StartDate")]
        public DateTime RegisterDate { get; set; }
        [Column("EndDate")]
        public DateTime ExpiredDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid FacilityID { get; set; }
        public required Facility Facility { get; set; }
    }

}