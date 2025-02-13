using System;
using Autofac;

namespace StatementIQ.Common.Autofac
{
    public class SingleInstanceAttribute : AutofacRegistrationAttribute
    {
        internal override void Register(ContainerBuilder builder, Type type)
        {
            var registrationBuilder = builder.RegisterType(type).SingleInstance();
            Register(registrationBuilder, type);
        }
    }
}