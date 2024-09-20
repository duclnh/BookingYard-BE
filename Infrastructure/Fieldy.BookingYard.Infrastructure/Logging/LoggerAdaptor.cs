using Fieldy.BookingYard.Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace Fieldy.BookingYard.Infrastructure.LoggerAdaptor
{
    public class LoggerAdaptor<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdaptor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}