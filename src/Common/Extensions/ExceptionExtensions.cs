using System;
using System.Linq;
using MandateThat;

namespace StatementIQ.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetExceptionDetails(this Exception exception)
        {
            Mandate.That(exception, nameof(exception)).IsNotNull();

            var properties = exception.GetType()
                .GetProperties();

            var fields = properties
                .Select(property => new
                {
                    property.Name,
                    Value = property.GetValue(exception, null)
                })
                .Select(x => $"{x.Name}: {x.Value?.ToString() ?? string.Empty}");

            return string.Join(Environment.NewLine, fields);
        }
    }
}