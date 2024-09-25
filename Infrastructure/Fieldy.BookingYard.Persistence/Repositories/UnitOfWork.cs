using Fieldy.BookingYard.Domain.Abstractions;
using Fieldy.BookingYard.Persistence.DatabaseContext;

namespace Fieldy.BookingYard.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingYardDBContext _dbContext;
        public UnitOfWork(BookingYardDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    result = await _dbContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
    }
}