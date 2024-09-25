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

    public async Task<decimal> GetMinPriceCourt(Guid FacilityID, CancellationToken cancellationToken)
    {
        var court =  await _dbContext.Courts.Where(x => x.FacilityID == FacilityID
                                            && x.IsActive && x.IsDelete == false)
                                        .OrderBy(x => x.CourtPrice)
                                        .FirstOrDefaultAsync(cancellationToken);
        
        return court == null ? 0 : court.CourtPrice;
    }
     public async Task<decimal> GetMaxPriceCourt(Guid FacilityID, CancellationToken cancellationToken)
    {
        var court =  await _dbContext.Courts.Where(x => x.FacilityID == FacilityID
                                            && x.IsActive && x.IsDelete == false)
                                        .OrderByDescending(x => x.CourtPrice)
                                        .FirstOrDefaultAsync(cancellationToken);
        
        return court == null ? 0 : court.CourtPrice;
    }
}
