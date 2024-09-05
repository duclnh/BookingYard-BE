namespace Fieldy.BookingYard.Application.Features.Support.Queries.GetAllSupport{
    public class SupportDTO {
        public int SupportID { get; set; }
        public required string  Name { get; set; }
        public required string Email { get; set; }

        public required string Phone { get; set; }

        public required string TypeSupport { get; set; }
        public required string Note {get; set;}
        public required string Status { get; set; }
        public string ? UserID { get; set; }    
        public string? UserName { get; set; }
        public string? Image {get ; set;}
        public DateTime CreateAt{ get; set; }
        public DateTime UpdateAt { get; set; }  
    }
}