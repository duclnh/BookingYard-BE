namespace Fieldy.BookingYard.Application.Features.Package.Queries
{
	public class PackageCreate
	{
		public Guid PackageId { get; set; }
		public string? PackageName { get; set; }
		public decimal PackagePrice { get; set; }
	}
}
