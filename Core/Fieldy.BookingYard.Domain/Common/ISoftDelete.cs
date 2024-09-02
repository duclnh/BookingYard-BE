namespace Fieldy.BookingYard.Domain.Common{
    public interface ISoftDelete {
        public bool IsDelete { get; set; }
        public void Undo(){
            this.IsDelete = false;
        }
    }
}