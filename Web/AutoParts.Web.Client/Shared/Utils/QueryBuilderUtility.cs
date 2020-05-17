namespace AutoParts.Web.Client.Shared.Utils
{
    using Microsoft.AspNetCore.WebUtilities;

    using System;
    using System.Reflection;
    using System.Collections.Generic;

    public class QueryBuilderUtility
    {
        private static readonly IReadOnlyDictionary<Type, Func<object, string>> valueTypesMappings = new Dictionary<Type, Func<object, string>>
        {
            {
                typeof(int),
                (value) => value.ToString() == default(int).ToString() ? string.Empty : value.ToString()
            },
            {
                typeof(long),
                (value) => value.ToString() == default(long).ToString() ? string.Empty : value.ToString()
            },
            {
                typeof(bool),
                (value) => value.ToString()
            }
        };

        public static string CreateQueryFromFilter<TFilter>(string uri, TFilter filter)
        {
            var filterType = typeof(TFilter);
            var filterProperties = filterType.GetProperties();

            foreach (var property in filterProperties)
            {
                var value = ResolveStringValueFromFilterProperty(filter, property);

                if (!string.IsNullOrEmpty(value))
                {
                    uri = QueryHelpers.AddQueryString(uri, property.Name, value);
                }
            }

            return uri;
        }

        private static string ResolveStringValueFromFilterProperty<TFilter>(TFilter filter, PropertyInfo property)
        {
            var value = property.GetValue(filter);

            if (property.PropertyType == typeof(string) || property.PropertyType.IsEnum)
            {
                return value == null ? string.Empty : value.ToString();
            }

            if (valueTypesMappings.ContainsKey(property.PropertyType))
            {
                return valueTypesMappings[property.PropertyType](value);
            }

            return string.Empty;
        }
    }
}
