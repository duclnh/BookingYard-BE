using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class VoucherRepository : RepositoryBase<Voucher, Guid>, IVoucherRepository
	{
		public VoucherRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{
		}
	}
}
