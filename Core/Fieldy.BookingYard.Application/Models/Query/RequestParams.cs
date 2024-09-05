namespace Fieldy.BookingYard.Application.Models.Query
{
    public class RequestParams
    {
        public string Search { get; set; } = string.Empty;
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        // public List<string>? SearchBy { get; set; }
    }
}