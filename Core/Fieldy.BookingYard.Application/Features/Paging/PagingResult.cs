namespace Fieldy.BookingYard.Application.Features.Paging
{
    public class PagingResult<T>
    {

        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public IList<T>? Results { get; set; }

        public static PagingResult<T> Create(int totalCount, int pageSize, int currentPage, int totalPages, bool hasNext, bool hasPrevious, IList<T>? results)
        {
            return new PagingResult<T>(totalCount, pageSize, currentPage, totalPages, hasNext, hasPrevious, results);
        }

        public PagingResult(int totalCount, int pageSize, int currentPage, int totalPages, bool hasNext, bool hasPrevious, IList<T>? results)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            HasNext = hasNext;
            HasPrevious = hasPrevious;
            Results = results;
        }
    }
}