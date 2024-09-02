namespace Fieldy.BookingYard.Domain.Common
{
    public interface IDateTracking
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}