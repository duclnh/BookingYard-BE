using Fieldy.BookingYard.Domain.Abstractions;
using Fieldy.BookingYard.Domain.Abstractions.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieldy.BookingYard.Domain.Entities
{
    [Table("Packages")]
    public class Package : EntityBase<int>, ISoftDelete, IDateTracking
	{
        public required string PackageName { get; set; }
		public required decimal PackagePrice { get; set; }
        public required string PackageDescription { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public ICollection<RegisterPackage>? RegisterPackages { get; set; }
	}

}