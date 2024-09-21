using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class FeedbackRepository : RepositoryBase<FeedBack, int>, IFeedbackRepository
	{
		private readonly BookingYardDBContext _dbContext;
		public FeedbackRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{
			_dbContext = bookingYardDBContext;
		}

		public async Task<ICollection<FeedBack>> GetFeedbackByFacilityID(Guid facilityID)
		{
			var result = await _dbContext.Feedbacks.Where(x => x.FacilityID == facilityID)
													.Take(10)
													.OrderByDescending(x => x.Rating)
													.Include(x => x.User)
													.ToListAsync();
			return result;
		}
	}
}
