using Microsoft.EntityFrameworkCore;

namespace Fieldy.BookingYard.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbContext GetContext();
    }
}


