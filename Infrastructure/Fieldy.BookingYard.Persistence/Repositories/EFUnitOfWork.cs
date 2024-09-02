using Fieldy.BookingYard.Application.Contracts.Persistence;
using Fieldy.BookingYard.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Persistence.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly BookingYardDBContext _dbContext;

        public EFUnitOfWork(BookingYardDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();

        public DbContext GetContext()
            => _dbContext;

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