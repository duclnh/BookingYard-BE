using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories{
	public interface IVoucherRepository : IRepositoryBase<Voucher, Guid>
	{
		Task<IList<Voucher>> GetVoucherTop(int numberTop, CancellationToken cancellationToken);
	}
}
