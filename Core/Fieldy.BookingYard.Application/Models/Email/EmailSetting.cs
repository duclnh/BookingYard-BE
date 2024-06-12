namespace Fieldy.BookingYard.Application.Models
{
    public class EmailSettings {
        public string FromEmailAddress { get; set;} = string.Empty;
        public string FromDisplayName { get; set;} = string.Empty;
        public Smtp Smtp{ get; set;} = new Smtp();
    }
}