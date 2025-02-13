using System;
using System.ComponentModel;
using Autofac;
using Autofac.Builder;

namespace StatementIQ.Common.Autofac
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class AutofacRegistrationAttribute : Attribute
    {
        protected AutofacRegistrationAttribute()
        {
            AsImplementedInterface = true;
        }

        public string Name { get; set; }

        [DefaultValue(true)] public bool AsImplementedInterface { get; set; }

        [DefaultValue(false)] public bool AutoActivate { get; set; }

        internal abstract void Register(ContainerBuilder builder, Type type);

        protected void Register<TLimit>(
            IRegistrationBuilder<TLimit, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder,
            Type type)
        {
            if (Name == null)
            {
                registrationBuilder.AsSelf();
            }
            else
            {
                registrationBuilder.Named(Name, type);
            }

            if (AsImplementedInterface)
            {
                registrationBuilder.AsImplementedInterfaces();
            }

            if (AutoActivate)
            {
                registrationBuilder.AutoActivate();
            }
        }
    }
}