namespace AutoParts.Web.Client.Shared.Utils
{
    using Microsoft.AspNetCore.Components;

    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Text.RegularExpressions;

    using Protos;

    using Private;
    using Private.User;
    using Private.Supplier;
    using Private.Administrator;

    using Public;

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

            if (IsUserSpecificPath<UserRoutes>(relativePath))
            {
                return userType.HasValue && userType.Value == UserType.User;
            }

            if (IsUserSpecificPath<AuthorizedRoutes>(relativePath))
            {
                return userType.HasValue;
            }

            if (IsUserSpecificPath<UnauthorizedRoutes>(relativePath))
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
