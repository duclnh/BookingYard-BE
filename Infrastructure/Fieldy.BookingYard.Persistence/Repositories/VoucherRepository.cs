using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class VoucherRepository : RepositoryBase<Voucher, Guid>, IVoucherRepository
	{
		public VoucherRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{
		}

		public async Task<IList<Voucher>> GetVoucherTop(int numberTop, CancellationToken cancellationToken)
		{
			return await _dbContext.Vouchers
								.AsNoTracking()
								.Where(x => x.Status && !x.IsDeleted && x.Code == null)
								.OrderByDescending(x => x.CreatedAt)
								.Take(numberTop)
								.Include(x => x.Facility)
								.Include(x => x.Sport)
								.ToListAsync(cancellationToken);

		}
	}
}
