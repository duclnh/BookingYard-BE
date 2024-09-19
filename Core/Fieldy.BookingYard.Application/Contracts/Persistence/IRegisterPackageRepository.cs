using Fieldy.BookingYard.Application.Features.RegisterPackage.Queries.GetAllRegisterPackage;
using Fieldy.BookingYard.Application.Models.Query;
using Fieldy.BookingYard.Domain.Entities;
using System.Linq.Expressions;

namespace Fieldy.BookingYard.Application.Contracts.Persistence
{
	public interface IRegisterPackageRepository : IRepositoryBase<RegisterPackage, Guid>
	{
	}
}
