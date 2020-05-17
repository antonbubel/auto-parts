namespace AutoParts.Web.Client.Shared.Utils
{
    using Microsoft.Extensions.Primitives;
    using Microsoft.AspNetCore.WebUtilities;

    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;

    public static class QueryParsingUtility
    {
        private static readonly IReadOnlyDictionary<Type, Func<string, object>> valueTypesMapping = new Dictionary<Type, Func<string, object>>
        {
            {
                typeof(int),
                (stringToParse) => int.Parse(stringToParse)
            },
            {
                typeof(long),
                (stringToParse) => long.Parse(stringToParse)
            },
            {
                typeof(bool),
                (stringToParse) => bool.Parse(stringToParse)
            }
        };

        public static TFilter ParseFilterFromUri<TFilter>(Uri uri)
            where TFilter : class
        {
            var filterType = typeof(TFilter);
            var propertyStringValuesDictionary = ParseUriAndGetPropertyStringValues(filterType, uri);

            var filter = Activator.CreateInstance(filterType);

            foreach (var queryProperty in propertyStringValuesDictionary)
            {
                var property = queryProperty.Key;
                var propertyStringValue = queryProperty.Value.First();
                var propertyValue = ParsePropertyValue(property, propertyStringValue);

                if (propertyValue != null)
                {
                    property.SetValue(filter, propertyValue);
                }
            }

            return (TFilter)filter;
        }

        private static IDictionary<PropertyInfo, StringValues> ParseUriAndGetPropertyStringValues(Type filterType, Uri uri)
        {
            var filterProperties = filterType.GetProperties();
            var parsedQuery = QueryHelpers.ParseQuery(uri.Query);
            var propertyStringValuesDictionary = new Dictionary<PropertyInfo, StringValues>();

            foreach (var property in filterProperties)
            {
                if (parsedQuery.TryGetValue(property.Name, out var value))
                {
                    propertyStringValuesDictionary.Add(property, value);
                }
            }

            return propertyStringValuesDictionary;
        }

        private static object ParsePropertyValue(PropertyInfo property, string value)
        {
            if (property.PropertyType == typeof(string))
            {
                return value;
            }

            if (property.PropertyType.IsEnum)
            {
                return Enum.Parse(property.PropertyType, value);
            }

            if (valueTypesMapping.ContainsKey(property.PropertyType))
            {
                try
                {
                    return valueTypesMapping[property.PropertyType](value);
                }
                catch (FormatException)
                {
                }
            }

            return null;
        }
    }
}
