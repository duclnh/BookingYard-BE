using System.Linq.Expressions;
using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories;

public interface IFacilityRepository : IRepositoryBase<Facility, Guid>
{
    Task<IList<Facility>> GetFacilitiesTop(int numberTake, CancellationToken cancellationToken = default);
    Task<IList<Facility>> FindAllFacility(
          Expression<Func<Facility, bool>>[]? expressions = null,
          Func<IQueryable<Facility>, IOrderedQueryable<Facility>>? orderBy = null,
          CancellationToken cancellationToken = default,
          params Expression<Func<Facility, object>>[] includes
      );
}
