using MediatR;

namespace Fieldy.BookingYard.Application.Features.Package.Commands.CreatePackage
{
    public class CreatePackageCommand : IRequest<string>
    {
        public string? PackageName { get; set; }
        public decimal PackagePrice { get; set; }
        public string? PackageDescription { get; set; }
    }
}
