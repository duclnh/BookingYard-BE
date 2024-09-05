namespace Fieldy.BookingYard.Domain.Common{
    public interface ISoftDelete {
        public bool IsDeleted { get; set; }
        public void Undo(){
            this.IsDeleted = false;
        }
    }
}