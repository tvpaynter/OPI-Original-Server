using System;
using System.ComponentModel;
using System.Linq;

// ReSharper disable once CheckNamespace

namespace LyonL.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            return !(Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) is DescriptionAttribute
                attribute)
                ? value.ToString()
                : attribute.Description;
        }

        public static string ToEnumDescriptionAttribute(this Enum val)
        {
            var attributes = (DescriptionAttribute[]) val
                .GetType()
                .GetField(val.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.FirstOrDefault()?.Description ?? string.Empty;
        }

        public static T ToEnumType<T>(this string description)
        {
            var type = typeof(T);

            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description) return (T) field.GetValue(null);
                }
                else
                {
                    if (field.Name == description) return (T) field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
        }

        public static T ToEnumType<T>(this int enumInt) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");

            if (!Enum.IsDefined(typeof(T), enumInt))
                throw new InvalidCastException($"{enumInt} is not an underlying value of the {typeof(T)} enumeration.");

            return (T) Enum.ToObject(typeof(T), enumInt);
        }
    }
}