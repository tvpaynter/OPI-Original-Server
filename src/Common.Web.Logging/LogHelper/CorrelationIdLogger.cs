using System;
using Microsoft.Extensions.Logging;

namespace StatementIQ.Common.Web.Logging.LogHelper
{
    /// <summary>
    /// Delegates to a new <see cref="ILogger"/> instance using the full name of the given type, created by the
    /// provided <see cref="ILoggerFactory"/>.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class CorrelationIdLogger<T> : ILogger<T>
    {
        private readonly ITraceIdentifierGetter _traceIdentifierGetter;

        public CorrelationIdLogger(
            ITraceIdentifierGetter traceIdentifierGetter) =>
            (_traceIdentifierGetter) = (traceIdentifierGetter);

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel.Information;
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

            Console.ForegroundColor = GetColor(logLevel);
            Console.Write($"{logLevel}. ");
            
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"[{DateTime.UtcNow.ToString("MM/dd/yy H:mm:ss fff")}][{typeof(T).Name}]");

            if (!string.IsNullOrEmpty(_traceIdentifierGetter.Identifier))
            {
                Console.Write($" [correlationId:${_traceIdentifierGetter.Identifier}]");
            }
            
            Console.WriteLine($" {formatter(state, exception)}");
        }
    }
}