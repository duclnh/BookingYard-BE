using Fieldy.BookingYard.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Packages")]
    public class Package : ISoftDelete, IDateTracking
	{
        public Guid PackageId { get; set; }
        public string? PackageName { get; set; }
		public decimal PackagePrice { get; set; }
        public string? PackageDescription { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
	}

}