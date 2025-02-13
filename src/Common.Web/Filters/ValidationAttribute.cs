using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using FluentValidation;
using FluentValidation.Results;
using MandateThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using StatementIQ.Codes;
using StatementIQ.Common.Autofac;
using StatementIQ.Errors;
using StatementIQ.Exceptions;
using StatementIQ.Extensions;

namespace StatementIQ.Common.Web.Filters
{
    [InstancePerLifetimeScope]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ValidationAttribute : ActionFilterAttribute
    {
        private static readonly string CollectionArgumentName = "collection";
        private static readonly string TypedPropertyArgumentName = "objectRoute";
        private readonly IValidatorsGetter _validatorsGetter;

        public ValidationAttribute(string argumentName, Type validatorType) : this(validatorType)
        {
            ArgumentName = argumentName;
        }

        public ValidationAttribute(Type validatorType)
        {
            Mandate.That(validatorType, nameof(validatorType)).IsNotNull();

            ValidatorType = validatorType;
        }

        protected ValidationAttribute(IValidatorsGetter validatorsGetter, string argumentName)
        {
            Mandate.That(argumentName, nameof(argumentName)).IsNotNullOrWhiteSpace();
            Mandate.That(validatorsGetter, nameof(validatorsGetter)).IsNotNull();

            _validatorsGetter = validatorsGetter;
            ArgumentName = argumentName;
        }

        private string ArgumentName { get; }
        private Type ValidatorType { get; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var errors = ValidateModelState(context.ModelState);

            if (errors.Any())
            {
                HandleBadRequest(context, errors);
            }
            else
            {
                var argument = GetArgument(context.ActionArguments ?? new Dictionary<string, object>());

                if (argument == null)
                {
                    throw new InvalidOperationException("Invalid Argument Name");
                }

                ValidationResult validationResult;

                if (argument is ExpandoObject)
                {
                    validationResult = ValidationForExpandoObject(context, argument);
                }
                else
                {
                    var validator = (IValidator) Activator.CreateInstance(ValidatorType);
                    validationResult = validator.Validate(new ValidationContext<object>(argument));
                }

                var errorsDictionary = new Dictionary<string, string>();

                foreach (var error in validationResult.Errors)
                {
                    if (errorsDictionary.ContainsKey(error.PropertyName))
                    {
                        errorsDictionary[error.PropertyName] =
                            $"{errorsDictionary[error.PropertyName]} {error.ErrorMessage}";
                    }
                    else
                    {
                        errorsDictionary.Add(error.PropertyName, error.ErrorMessage);
                    }
                }

                if (!validationResult.IsValid)
                {
                    HandleBadRequest(context, errorsDictionary);
                }
            }

            base.OnActionExecuting(context);
        }

        private ValidationResult ValidationForExpandoObject(ActionExecutingContext context, object argument)
        {
            var validationResult = new ValidationResult();
            var collectionName = GetCollectionName(context.ActionArguments ?? new Dictionary<string, object>());
            var typedPropertyName = GetTypedPropertyName(context.ActionArguments ?? new Dictionary<string, object>());

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new InvalidOperationException("Collection argument not provided");
            }

            if (!string.IsNullOrEmpty(typedPropertyName))
            {
                collectionName = $"{collectionName}/{typedPropertyName}";
            }

            if (!_validatorsGetter.GetValidators().TryGetValue(collectionName, out var validatorType))
            {
                return validationResult;
            }

            var validator = (IValidator) Activator.CreateInstance(validatorType);

            if (validatorType.BaseType != null)
            {
                var entityType = validatorType.BaseType.GetGenericArguments().FirstOrDefault();

                var dataJsonString = JsonConvert.SerializeObject((dynamic) argument);
                object entityInstance;
                try
                {
                    entityInstance = JsonConvert.DeserializeObject(dataJsonString, entityType);
                }
                catch (JsonReaderException jsonReaderException)
                {
                    validationResult.Errors.Add(new ValidationFailure(jsonReaderException.Path,
                        jsonReaderException.Message));
                    return validationResult;
                }

                var rules = new List<IValidationRule>();
                if (validator != null)
                {
                    var rule = validator.CreateDescriptor();

                    foreach (var property in (IDictionary<string, object>) argument)
                    {
                        var ruleForMember = rule.GetRulesForMember(property.Key.ToPascalCase()).ToArray();

                        if (!ruleForMember.Any())
                        {
                            var propertyRoute = $"{collectionName}/{property.Key.ToLower()}";
                            _validatorsGetter.GetValidators().TryGetValue(propertyRoute, out _);
                        }
                        else
                        {
                            rules.AddRange(ruleForMember);
                        }
                    }
                }

                var parentValidationResult =
                    new ValidationResult(
                        rules.SelectMany(x => x.Validate(new ValidationContext<object>(entityInstance))));

                if (parentValidationResult.Errors.Any())
                {
                    foreach (var validationFailure in parentValidationResult.Errors)
                    {
                        validationResult.Errors.Add(validationFailure);
                    }
                }
            }

            return validationResult;
        }

        /// <summary>
        ///     Validate incoming ModelState. Parse JsonSerializationExceptions
        /// </summary>
        /// <returns>return Validation Result</returns>
        private ResponseErrors ValidateModelState(ModelStateDictionary modelState)
        {
            var errors = new ResponseErrors();

            foreach (var model in modelState)
            {
                var customJsonSerializingExceptions = model.Value.Errors.Where(er =>
                        er.Exception is StringEnumConverterSerializationException
                        || er.Exception is InvalidPropertyTypeException)
                    .ToList();

                if (customJsonSerializingExceptions.Any())
                {
                    errors.AddRange(customJsonSerializingExceptions.Select(e =>
                        new ResponseError(BadRequestCodes.InvalidRequestParameter, e.Exception.Message)));
                }
                else if (model.Value.Errors.Any(er => er.Exception is JsonException))
                {
                    errors.Add(new ResponseError(BadRequestCodes.InvalidRequestParameter,
                        $"{model.Key} has invalid value."));
                }
            }

            return errors;
        }

        private object GetArgument(IDictionary<string, object> actionArguments)
        {
            return !string.IsNullOrWhiteSpace(ArgumentName) && actionArguments.ContainsKey(ArgumentName)
                ? actionArguments[ArgumentName]
                : actionArguments.Values.FirstOrDefault();
        }

        private static string GetCollectionName(IDictionary<string, object> actionArguments)
        {
            return !string.IsNullOrWhiteSpace(CollectionArgumentName)
                   && actionArguments.ContainsKey(CollectionArgumentName)
                ? actionArguments[CollectionArgumentName].ToString()
                : string.Empty;
        }

        private string GetTypedPropertyName(IDictionary<string, object> actionArguments)
        {
            var typedPropertyName = !string.IsNullOrWhiteSpace(TypedPropertyArgumentName)
                                    && actionArguments.ContainsKey(TypedPropertyArgumentName)
                ? Regex.Replace(
                    HttpUtility.UrlDecode(actionArguments[TypedPropertyArgumentName].ToString()) ?? string.Empty,
                    @"[\d-]",
                    string.Empty).Replace("//", "/")
                : string.Empty;

            if (typedPropertyName.Length > 0 && typedPropertyName.Last() == '/')
            {
                typedPropertyName = typedPropertyName.Remove(typedPropertyName.Length - 1);
            }

            return typedPropertyName;
        }

        private void HandleBadRequest(ActionExecutingContext context, Dictionary<string, string> errors)
        {
            var resp = new ErrorsResponse
                {Code = 400.10M, Message = "invalid fields"};
            var resErrors = new Dictionary<string, object>();
            foreach (var pair in errors)
            {
                var key = pair.Key.ToCamelCase();
                var parts = key.Split('.');
                var currentObj = resErrors;
                for (var i = 0; i < parts.Length - 1; i++)
                {
                    var property = parts[i].ToCamelCase();
                    if (!currentObj.Keys.Contains(property))
                    {
                        currentObj[property] = new Dictionary<string, object>();
                    }

                    currentObj = (Dictionary<string, object>) currentObj[property];
                }

                currentObj[parts[^1].ToCamelCase()] = pair.Value;
            }

            resp.Fields = resErrors;
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.Result = new JsonResult(resp);
        }

        private void HandleBadRequest(ActionExecutingContext context, ResponseErrors errors)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new
                {Code = 400.10M, Message = "invalid fields", Fields = errors});
        }
    }
}