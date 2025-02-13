using Autofac;
using MandateThat;

namespace StatementIQ.Common.Data.DependencyInjection
{
    public static class PostgresRetryExtensions
    {
        public static void RegisterPostgresRetry(this ContainerBuilder builder, int retryCount, int retryDelay)
        {
            Mandate.That(builder, nameof(builder)).IsNotNull();

            builder.RegisterType<PostgresRetry>().WithParameter(
                    (pi, c) => pi.ParameterType == typeof(int) && pi.Name == "defaultRetryCount",
                    (pi, c) => retryCount).WithParameter(
                    (pi, c) => pi.ParameterType == typeof(int) && pi.Name == "defaultRetryDelay",
                    (pi, c) => retryDelay)
                .AsImplementedInterfaces().InstancePerDependency();
        }
    }
}