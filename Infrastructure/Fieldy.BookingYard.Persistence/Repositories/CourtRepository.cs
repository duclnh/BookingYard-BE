using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories;

public class CourtRepository : RepositoryBase<Court, int>, ICourtRepository
{
    public CourtRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
    {
    }

    public async Task<IList<Court>> GetAllCourts(Guid FacilityID, CancellationToken cancellationToken = default)
    {

        return await _dbContext.Courts.Where(c => c.FacilityID == FacilityID && c.IsDelete == false)
                                    .Include(c => c.Sport)
                                    .OrderByDescending(c => c.Id)
                                    .OrderByDescending(c => c.IsActive)
                                    .ToListAsync(cancellationToken);
    }
}
