using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace StatementIQ.Common.Web.Logging.LogHelper
{
    /// <summary>
    /// Delegates to a new <see cref="ILogger"/> instance using the full name of the given type, created by the
    /// provided <see cref="ILoggerFactory"/>.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class SerilogLogger<T> : ILogger<T>
    {
        private readonly ITraceIdentifierGetter _traceIdentifierGetter;
        private static Serilog.ILogger _logger;
        private readonly string _microservice;
        private readonly string _environment;
        public SerilogLogger(
            ITraceIdentifierGetter traceIdentifierGetter, IConfiguration config)
        {
            if (_logger == null)
                _logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .CreateLogger();

            _traceIdentifierGetter = traceIdentifierGetter;
            _microservice = config["service"];
            _environment = config["environment"];
        }
        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel)
        {
            LogEventLevel level = (LogEventLevel)(int)logLevel;
            return _logger.IsEnabled(level);
        }

        private ConsoleColor GetColor(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Information:
                    return ConsoleColor.Green;
                case LogLevel.Warning:
                    return ConsoleColor.Yellow;
                case LogLevel.Error:
                    return ConsoleColor.Red;
                case LogLevel.Critical:
                    return ConsoleColor.DarkRed;
                default:
                    return ConsoleColor.Gray;
            }
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var logstring = $"[{_environment} {_microservice}][{DateTime.UtcNow.ToString("MM/dd/yy H:mm:ss fff")}][{typeof(T).Name}]  [correlationId:${_traceIdentifierGetter.Identifier}] {state}";
            switch (logLevel)
            {
                case LogLevel.Trace:
                    _logger.Verbose(exception, logstring);
                    break;
                case LogLevel.Debug:
                    _logger.Debug(exception, logstring);
                    break;
                case LogLevel.Information:
                    _logger.Information(exception, logstring);
                    break;
                case LogLevel.Warning:
                    _logger.Warning(exception, logstring);
                    break;
                case LogLevel.Error:
                    _logger.Error(exception, logstring);
                    break;
                case LogLevel.Critical:
                    _logger.Error(exception, logstring);
                    break;
                case LogLevel.None:
                    break;
                default:
                    break;
            }
        }
    }
}