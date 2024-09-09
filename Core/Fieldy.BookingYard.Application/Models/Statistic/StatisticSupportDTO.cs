namespace Fieldy.BookingYard.Application.Models.Statistic{
    public class StatisticSupportDTO{
        public int TotalSupport  { get; set; }
        public int TotalProcessed { get; set; }
        public int TotalUnProcessed { get; set; }
        public StatisticSupportDTO(int totalSupport, int totalProcessed) 
        {
            this.TotalSupport = totalSupport;
            this.TotalProcessed = totalProcessed;
            this.TotalUnProcessed = totalSupport - totalProcessed;
        }
    }
}