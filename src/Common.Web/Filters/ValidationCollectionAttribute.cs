using System;
using StatementIQ.Common.Autofac;

namespace StatementIQ.Common.Web.Filters
{
    [InstancePerLifetimeScope]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ValidationCollectionAttribute : ValidationAttribute
    {
        public ValidationCollectionAttribute(IValidatorsGetter validatorsGetter, string argumentName) : base(validatorsGetter, argumentName)
        {
        }
    }
}