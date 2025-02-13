using Autofac;
using MandateThat;

namespace StatementIQ.Common.Data.DependencyInjection
{
    public static class PostgresUnitOfWorkExtensions
    {
        public static void RegisterPostgresUnitOfWork(this ContainerBuilder builder)
        {
            Mandate.That(builder, nameof(builder)).IsNotNull();

            builder.RegisterType<PostgresUnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}