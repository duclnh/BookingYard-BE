namespace Fieldy.BookingYard.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string name, object key) : base($"{name} ({key}) was exist")
        {

        }
    }
}
