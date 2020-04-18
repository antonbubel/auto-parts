using AutoParts.Web.Client.Shared.Constants;
using Blazored.LocalStorage;
using Grpc.Core;


namespace AutoParts.Web.Client.Shared.Utils
{
    public static class LocalStorageExtensions
    {
        public static string GetAccessToken(this ISyncLocalStorageService localStorage)
        {
            return localStorage.GetItem<string>(LocalStorageConstants.AccessToken);
        }

        public static string GetTokenType(this ISyncLocalStorageService localStorage)
        {
            return localStorage.GetItem<string>(LocalStorageConstants.TokenType);
        }

        public static string GetRefreshToken(this ISyncLocalStorageService localStorage)
        {
            return localStorage.GetItem<string>(LocalStorageConstants.RefreshToken);
        }

        public static bool HasAccessToken(this ISyncLocalStorageService localStorage)
        {
            return localStorage.ContainKey(LocalStorageConstants.TokenType)
                && localStorage.ContainKey(LocalStorageConstants.AccessToken);
        }

        public static bool HasRefreshToken(this ISyncLocalStorageService localStorage)
        {
            return localStorage.ContainKey(LocalStorageConstants.RefreshToken);
        }

        public static void SetAuthorizationTokens(this ISyncLocalStorageService localStorage, string tokenType, string accessToken, string refreshToken)
        {
            localStorage.SetItem(LocalStorageConstants.TokenType, tokenType);
            localStorage.SetItem(LocalStorageConstants.AccessToken, accessToken);
            localStorage.SetItem(LocalStorageConstants.RefreshToken, refreshToken);
        }

        public static void RemoveAuthorizationTokens(this ISyncLocalStorageService localStorage)
        {
            if (localStorage.HasAccessToken())
            {
                localStorage.RemoveItem(LocalStorageConstants.TokenType);
                localStorage.RemoveItem(LocalStorageConstants.AccessToken);
            }

            if (localStorage.HasRefreshToken())
            {
                localStorage.RemoveItem(LocalStorageConstants.RefreshToken);
            }
        }
    }
}
