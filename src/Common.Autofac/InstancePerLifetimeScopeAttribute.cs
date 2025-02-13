using System;
using Autofac;

namespace StatementIQ.Common.Autofac
{
    public class InstancePerLifetimeScopeAttribute : AutofacRegistrationAttribute
    {
        internal override void Register(ContainerBuilder builder, Type type)
        {
            var registrationBuilder = builder.RegisterType(type).InstancePerLifetimeScope();
            Register(registrationBuilder, type);
        }
    }
}