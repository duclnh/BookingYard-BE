using Fieldy.BookingYard.Domain.Abstractions.Repositories;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories
{
	public class TransactionRepository : RepositoryBase<Transaction, Guid>, ITransactionRepository
	{
		public TransactionRepository(BookingYardDBContext bookingYardDBContext) : base(bookingYardDBContext)
		{

		}
	}
}
