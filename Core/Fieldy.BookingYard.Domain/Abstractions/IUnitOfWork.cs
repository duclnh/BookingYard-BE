namespace Fieldy.BookingYard.Domain.Abstractions
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}


