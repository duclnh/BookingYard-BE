using Fieldy.BookingYard.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Packages")]
    public class Package : EntityBase<Guid>, ISoftDelete, IDateTracking
	{
        public required string PackageName { get; set; }
		public required decimal PackagePrice { get; set; }
        public required string PackageDescription { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
	}

}