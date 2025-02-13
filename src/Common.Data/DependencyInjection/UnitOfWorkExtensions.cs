using Autofac;
using MandateThat;

namespace StatementIQ.Common.Data.DependencyInjection
{
    public static class UnitOfWorkExtensions
    {
        public static void RegisterUnitOfWork(this ContainerBuilder builder)
        {
            Mandate.That(builder, nameof(builder)).IsNotNull();

            builder.RegisterType<UnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}