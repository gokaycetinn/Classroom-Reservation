using Serilog;

namespace ClassroomReservation.Services
{
    public interface ILoggingService
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, Exception? exception = null, params object[] args);
        void LogDebug(string message, params object[] args);
    }    public class LoggingService : ILoggingService
    {
        private readonly Serilog.ILogger _logger;

        public LoggingService()
        {
            _logger = Log.Logger;
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.Information(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.Warning(message, args);
        }

        public void LogError(string message, Exception? exception = null, params object[] args)
        {
            if (exception != null)
            {
                _logger.Error(exception, message, args);
            }
            else
            {
                _logger.Error(message, args);
            }
        }

        public void LogDebug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }
    }
}
