namespace AutoParts.Utilities.Common.Extensions
{
    using System;
    using System.Linq;

    /// <summary>
    /// Enum extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets attribute for the enum field.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <typeparam name="TAttribute">Attribute type.</typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TEnum, TAttribute>(this TEnum @enum)
        {
            var sortingType = @enum.GetType();

            if (!sortingType.IsEnum)
            {
                throw new ArgumentException($"{nameof(@enum)} must be an enumerated type");
            }

            var attribute = sortingType
                .GetField(@enum.ToString())
                .GetCustomAttributes(typeof(TAttribute), false)
                .Cast<TAttribute>()
                .FirstOrDefault();

            return attribute;
        }
    }
}

