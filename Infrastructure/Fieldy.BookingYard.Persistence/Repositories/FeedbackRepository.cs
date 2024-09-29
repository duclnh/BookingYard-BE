using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enums;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class FeedbackRepository : RepositoryBase<FeedBack, int>, IFeedbackRepository
	{
		public FeedbackRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{

		}

		public async Task<float> GetRatingFacility(Guid facilityID, CancellationToken cancellationToken)
		{
			var feedbackData = await _dbContext.Feedbacks
					.AsNoTracking()
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

			float averageRating = (float)feedbackData.SumRating / feedbackData.Count;
			return (float)Math.Round(averageRating, 1, MidpointRounding.AwayFromZero);
		}

		public async Task<(int one, int two, int three, int four, int five)> GetRatingFacilityStarsCount(Guid facilityID, CancellationToken cancellationToken)
		{
			var starCounts = await _dbContext.Feedbacks
						.AsNoTracking()
						.Where(x => x.FacilityID == facilityID)
						.GroupBy(x => x.Rating)
						.Select(g => new
						{
							Star = g.Key,
							Count = g.Count()
						})
						.ToDictionaryAsync(x => x.Star, x => x.Count, cancellationToken);

			var totalFeedbacks = starCounts.Values.Sum();

			if (totalFeedbacks == 0)
			{
				return (0, 0, 0, 0, 0);
			}

			var oneStarPercentage = starCounts.ContainsKey(1) ? (int)Math.Round((float)starCounts[1] / totalFeedbacks * 100) : 0;
			var twoStarPercentage = starCounts.ContainsKey(2) ? (int)Math.Round((float)starCounts[2] / totalFeedbacks * 100) : 0;
			var threeStarPercentage = starCounts.ContainsKey(3) ? (int)Math.Round((float)starCounts[3] / totalFeedbacks * 100) : 0;
			var fourStarPercentage = starCounts.ContainsKey(4) ? (int)Math.Round((float)starCounts[4] / totalFeedbacks * 100) : 0;
			var fiveStarPercentage = starCounts.ContainsKey(5) ? (int)Math.Round((float)starCounts[5] / totalFeedbacks * 100) : 0;

			return (oneStarPercentage, twoStarPercentage, threeStarPercentage, fourStarPercentage, fiveStarPercentage);
		}

		public async Task<IList<FeedBack>> GetTopFeedBack(TypeFeedback typeFeedback, int numberTake, CancellationToken cancellationToken)
		{
			return await _dbContext.Feedbacks
						.AsNoTracking()
						.Where(x => x.TypeFeedback == typeFeedback && x.IsShow)
						.OrderByDescending(x => x.Rating)
						.Include(x => x.User)
						.Distinct()
						.Take(numberTake)
						.ToListAsync(cancellationToken);
		}
	}
}
