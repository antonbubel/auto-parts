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

            if (localStorage.HasAccessToken())
            {
                var tokenType = localStorage.GetTokenType();
                var accessToken = localStorage.GetAccessToken();

                headers = SetAuthorizationHeader(headers, tokenType, accessToken);
            }

            return headers;
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
