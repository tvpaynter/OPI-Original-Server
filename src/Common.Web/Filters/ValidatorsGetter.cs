using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StatementIQ.Common.Autofac;

namespace StatementIQ.Common.Web.Filters
{
    [SingleInstance]
    public class ValidatorsGetter : IValidatorsGetter
    {
        public Dictionary<string, Type> GetValidators()
        {
            var result = new Dictionary<string, Type>();
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly =>
                    !string.IsNullOrEmpty(assembly.FullName) && !assembly.FullName.StartsWith("Microsoft"))
                .SelectMany(x => x.GetTypes())
                .Select(x => new
                {
                    Type = x,
                    Attributes = x.GetCustomAttributes<ValidateCollectionAttribute>(false)
                });

            foreach (var type in types)
            {
                var validatorAttribute = type.Attributes.FirstOrDefault();

                if (validatorAttribute != null)
                {
                    var collectionNames = validatorAttribute.CollectionNames;

                    foreach (var collectionName in collectionNames)
                    {
                        if (!result.ContainsKey(collectionName))
                        {
                            result.TryAdd(collectionName, type.Type);
                        }
                    }
                }
            }

            return result;
        }
    }
}