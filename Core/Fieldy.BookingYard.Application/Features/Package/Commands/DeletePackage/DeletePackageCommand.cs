using MediatR;

namespace Fieldy.BookingYard.Application.Features.Package.Commands.DeletePackage
{
	public class DeletePackageCommand : IRequest<string>
	{
		public int PackageId { get; set; }
	}
}
