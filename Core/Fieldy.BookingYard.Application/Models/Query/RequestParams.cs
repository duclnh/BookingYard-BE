namespace Fieldy.BookingYard.Application.Models.Query
{
    public class RequestParams
    {
        public string? Search { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}