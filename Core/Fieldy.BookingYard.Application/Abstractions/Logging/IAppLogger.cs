namespace Fieldy.BookingYard.Application.Abstractions
{
    public interface IAppLogger<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);

    }
}