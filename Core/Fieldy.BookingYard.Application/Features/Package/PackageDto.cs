namespace Fieldy.BookingYard.Application.Features.Package
{
	public class PackageDto
    {
        public Guid? PackageId { get; set; }
        public string? PackageName { get; set; }
        public decimal PackagePrice { get; set; }
        public string? PackageDescription { get; set; }
    }
}
