namespace Fieldy.BookingYard.Domain.Common{
    public interface IUserTracking {
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }
}