using MediatR;

namespace Fieldy.BookingYard.Application.Features.Package.Commands.UpdatePackage
{
	public class UpdatePackageCommand : IRequest<string>
	{
		public Guid PackageId { get; set; }
		public string? PackageName { get; set; }
		public decimal PackagePrice { get; set; }
		public string? PackageDescription { get; set; }
		public DateTime ModifiedAt { get; set; }
	}
}
