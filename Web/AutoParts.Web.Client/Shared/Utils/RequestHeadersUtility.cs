using AutoParts.Web.Client.Shared.Constants;
using Blazored.LocalStorage;
using Grpc.Core;

namespace AutoParts.Web.Client.Shared.Utils
{
    public static class RequestHeadersUtility
    {
        public static Metadata GetRequestHeaders(ISyncLocalStorageService localStorage)
        {
            Metadata headers = null;

            if (HasAccessToken(localStorage))
            {
                var tokenType = localStorage.GetItem<string>(LocalStorageConstants.TokenType);
                var accessToken = localStorage.GetItem<string>(LocalStorageConstants.AccessToken);

                headers = SetAuthorizationHeader(headers, tokenType, accessToken);
            }

            return headers;
        }

        private static bool HasAccessToken(ISyncLocalStorageService localStorage)
        {
            return localStorage.ContainKey(LocalStorageConstants.TokenType)
                && localStorage.ContainKey(LocalStorageConstants.AccessToken);
        }

        private static Metadata SetAuthorizationHeader(Metadata headers, string tokenType, string accessToken)
        {
            if (headers == null)
            {
                headers = new Metadata();
            }

            headers.Add(AuthorizationConstants.AuthorizationHeaderKey, $"{tokenType} {accessToken}");

            return headers;
        }
    }
}
