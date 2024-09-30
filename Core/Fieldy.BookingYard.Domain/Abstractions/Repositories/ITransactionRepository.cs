using Fieldy.BookingYard.Domain.Entities;

namespace Fieldy.BookingYard.Domain.Abstractions.Repositories
{
	public interface ITransactionRepository : IRepositoryBase<Transaction, Guid>
	{
	}
}
