namespace Fieldy.BookingYard.Application.Features.Support.Queries.GetAllSupport{
    public class SupportDTO {
        public int Id { get; set; }
        public required string  Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Content { get; set; }
        public required string TypeSupport { get; set; }
        public required string Note {get; set;} 
        public required bool IsProcessed { get; set; }
        public Guid? ModifiedBy { get; set; }    
        public required string CreatedAt { get; set; }
        public string? ModifiedAt { get; set; }  
    }
}