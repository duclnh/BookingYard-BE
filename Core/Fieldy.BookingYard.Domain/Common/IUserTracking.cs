namespace Fieldy.BookingYard.Domain.Common{
    public interface IUserTracking {
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}