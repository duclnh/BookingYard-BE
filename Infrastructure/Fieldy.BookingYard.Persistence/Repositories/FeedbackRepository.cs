using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class FeedbackRepository : RepositoryBase<FeedBack, int>, IFeedbackRepository
	{
		public FeedbackRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{

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

		public async Task<float> GetRatingFacility(Guid facilityID, CancellationToken cancellationToken)
		{
			var feedbackData = await _dbContext.Feedbacks
					.Where(x => x.FacilityID == facilityID)
					.GroupBy(x => x.FacilityID)
					.Select(g => new
					{
						Count = g.Count(),
						SumRating = g.Sum(x => x.Rating)
					})
					.FirstOrDefaultAsync(cancellationToken);

			// Check if there are any feedbacks to avoid division by zero
			if (feedbackData == null || feedbackData.Count == 0)
			{
				return 0f; // No feedbacks, return 0 rating
			}

			// Calculate the average rating
			return (float)feedbackData.SumRating / feedbackData.Count;
		}
	}
}
