using Fieldy.BookingYard.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace Fieldy.BookingYard.Persistence.Repositories
{
    public class PagingList<T> : IPagingList<T>
    {

        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public IList<T> Results { get; set; }
        public PagingList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            CurrentPage = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            HasPrevious = CurrentPage > 1;
            HasNext = TotalPages - CurrentPage > 0;
            TotalCount = count;
            Results = items;
        }
        public static async Task<IPagingList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken);
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PagingList<T>(items, count, pageIndex, pageSize);
        }
    }
}