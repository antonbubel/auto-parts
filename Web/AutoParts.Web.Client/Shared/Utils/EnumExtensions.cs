namespace AutoParts.Web.Client.Shared.Utils
{
    using System;
    using System.Linq;

    public static class EnumExtensions
    {
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
