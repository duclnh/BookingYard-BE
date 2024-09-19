using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class RegisterPackageRepository : RepositoryBase<RegisterPackage, Guid>, IRegisterPackageRepository
	{
		public RegisterPackageRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{
		}
	}
}
