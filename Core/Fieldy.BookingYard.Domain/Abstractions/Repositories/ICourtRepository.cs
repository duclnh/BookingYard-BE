using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories;

public interface ICourtRepository : IRepositoryBase<Court, int>
{
    /// <summary>
    /// Retrieves all courts associated with a specific facility, identified by FacilityID.
    /// </summary>
    /// <param name="FacilityID">The facility identifier of type <c>Guid</c>.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A list of courts associated with the given facility.</returns>
    public Task<IList<Court>> GetAllCourts(Guid FacilityID, CancellationToken cancellationToken = default(CancellationToken));
    public Task<decimal> GetMinPriceCourt(Guid FacilityID, CancellationToken cancellationToken);
    public Task<decimal> GetMaxPriceCourt(Guid FacilityID, CancellationToken cancellationToken);
    public Task<IList<string>> GetSports(Guid FacilityID, CancellationToken cancellationToken);
}
