using System.Collections;

namespace Fieldy.BookingYard.Application.Contracts.Persistence
{
    public interface IPagingList<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public IList<T> Results { get; set; }
    }
}


