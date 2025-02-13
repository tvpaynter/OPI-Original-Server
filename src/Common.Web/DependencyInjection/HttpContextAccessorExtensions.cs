using Autofac;
using MandateThat;
using Microsoft.AspNetCore.Http;

namespace StatementIQ.Common.Web.DependencyInjection
{
    public static class HttpContextAccessorExtensions
    {
        public static void RegisterHttpContextAccessor(this ContainerBuilder builder)
        {
            Mandate.That(builder, nameof(builder)).IsNotNull();

            builder.RegisterType<HttpContextAccessor>().AsImplementedInterfaces().SingleInstance();
        }
    }
}