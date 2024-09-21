using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories;

public interface IFacilityRepository : IRepositoryBase<Facility, Guid>
{
	Task<Facility> GetFacilityByID(Guid facilityID);
}
