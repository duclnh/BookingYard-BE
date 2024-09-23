using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	internal class ImageRepository : RepositoryBase<Image, int>, IImageRepository
	{
		private readonly BookingYardDBContext _dbContext;
		public ImageRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{
			_dbContext = bookingYardDBContext;
		}
		public async Task<IList<Image>> GetImagesByFacilityID(Guid facilityID)
		{
			var result = await _dbContext.Images.Where(x => x.FacilityID == facilityID)
													.Include(x => x.FeedBack)
													.Include(x => x.Report)
													.ToListAsync();
			return result;
		}
	}
}
