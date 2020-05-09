namespace AutoParts.Web.Client.Shared.Utils
{
    using AutoParts.Web.Client.Private.Administrator;
    using AutoParts.Web.Protos;
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using AutoParts.Web.Client.Private.Supplier;
    using AutoParts.Web.Client.Public.User;

    public static class NavigationManagerExtensions
    {
        public static bool CanActivateCurrentRoute(this NavigationManager navigationManager, UserType? userType)
        {
            var relativePath = navigationManager.ToBaseRelativePath(navigationManager.Uri);

            if (IsUserSpecificPath<AdministratorRoutes>(relativePath))
            {
                return userType.HasValue && userType.Value == UserType.Administrator;
            }

            if (IsUserSpecificPath<SupplierRoutes>(relativePath))
            {
                return userType.HasValue && userType.Value == UserType.Supplier;
            }

            if (IsUserSpecificPath<UnauthorizedUserRoutes>(relativePath))
            {
                return !userType.HasValue;
            }

            return true;
        }

        private static bool IsUserSpecificPath<TEnum>(string path)
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(enumValue => enumValue.GetAttribute<TEnum, DescriptionAttribute>())
                .Select(attribute => CreateRegexForRoute(attribute.Description))
                .Any(routeRegex => routeRegex.IsMatch(path));
        }

        private static Regex CreateRegexForRoute(string route)
        {
            route = route.Replace("/", @"\/");
            route = route.Replace("*", @".+");
            route = $@"^{route}\/?$";

            return new Regex(route);
        }
    }
}
