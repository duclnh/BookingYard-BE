using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories
{
	public interface IImageRepository : IRepositoryBase<Image, int>
	{
		public Task<IList<Image>> GetImagesByFacilityID(Guid facilityID);
	}
}
