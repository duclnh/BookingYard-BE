using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class PackageRepository : RepositoryBase<Package, Package>, IPackageRepository
	{
		public PackageRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{
		}
	}
}
