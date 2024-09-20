using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class RegisterPackageRepository : RepositoryBase<RegisterPackage, Guid>, IRegisterPackageRepository
	{
		public RegisterPackageRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{
		}
	}
}
