namespace Fieldy.BookingYard.Domain.Abstractions.Entities{
    public interface ISoftDelete {
        public bool IsDeleted { get; set; }
        public void Undo(){
            this.IsDeleted = false;
        }
    }
}