namespace AutoParts.Utilities.Common.Extensions
{
    using System;
    using System.Linq;

    public static class TypeExtensions
    {
        public static bool ImplementGenericInterface(this Type type, Type genericInterfaceType)
        {
            return type.GetInterfaces()
                .Any(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericInterfaceType);
        }

        public static Type GetGenericInterfaceDefinition(this Type type, Type genericInterfaceType)
        {
            if (!type.ImplementGenericInterface(genericInterfaceType))
            {
                throw new ArgumentException(
                    $"Providen type {type.FullName} does not implement generic interface {genericInterfaceType.FullName}",
                    type.Name
                );
            }

            return type.GetInterfaces()
                .First(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericInterfaceType);
        }

        public static Type[] GetGenericInterfaceArguments(this Type type, Type genericInterfaceType)
        {
            var genericInterface = type.GetGenericInterfaceDefinition(genericInterfaceType);

            return genericInterface
                .GetGenericArguments();
        }
    }
}

