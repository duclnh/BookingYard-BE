using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories
{
	public interface IFeedbackRepository : IRepositoryBase<FeedBack, int>
	{
		public Task<ICollection<FeedBack>> GetFeedbackByFacilityID(Guid facilityID);
		public Task<float> GetRatingFacility(Guid facilityID, CancellationToken cancellationToken);
		public Task<(int one, int two, int three, int four, int five)> GetRatingFacilityStarsCount(Guid facilityID, CancellationToken cancellationToken);
	}
}
