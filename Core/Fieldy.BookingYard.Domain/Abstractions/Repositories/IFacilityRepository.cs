using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories;

public interface IFacilityRepository : IRepositoryBase<Facility, Guid>
{
    Task<IList<Facility>> GetFacilitiesTop(int numberTake, CancellationToken cancellationToken = default);
}
