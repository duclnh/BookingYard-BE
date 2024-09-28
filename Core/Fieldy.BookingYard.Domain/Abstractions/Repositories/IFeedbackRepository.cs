using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Domain.Enums;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories
{
	public interface IFeedbackRepository : IRepositoryBase<FeedBack, int>
	{
		public Task<float> GetRatingFacility(Guid facilityID, CancellationToken cancellationToken);
		public Task<(int one, int two, int three, int four, int five)> GetRatingFacilityStarsCount(Guid facilityID, CancellationToken cancellationToken);
		public Task<IList<FeedBack>> GetTopFeedBack(TypeFeedback typeFeedback, int numberTake, CancellationToken cancellationToken);
	}
}
