namespace AutoParts.Web.Client.Shared.Services
{
    using Blazored.LocalStorage;

    using Grpc.Net.Client;

    using System.Threading.Tasks;

    using Protos;

    using Utils;

    public class CurrentUserService
    {
        private readonly ISyncLocalStorageService localStorage;
        private readonly GrpcUserService.GrpcUserServiceClient userServiceClient;
        private readonly CurrentUserProvider currentUserProvider;

        public CurrentUserService(ISyncLocalStorageService localStorage, GrpcChannel channel, CurrentUserProvider currentUserProvider)
        {
            userServiceClient = new GrpcUserService.GrpcUserServiceClient(channel);
            this.localStorage = localStorage;
            this.currentUserProvider = currentUserProvider;
        }

        public async Task SyncCurrentUserInfo()
        {
            if (!localStorage.HasAccessToken())
            {
                currentUserProvider.SetUserInfoLoading(false);

                return;
            }

            currentUserProvider.SetUserInfoLoading(true);

            var response = await GetCurrentUserInfo();

            if (response == null)
            {
                var success = await GetRefreshedToken();

                if (success)
                {
                    response = await GetCurrentUserInfo();
                }
            }

            if (response == null)
            {
                currentUserProvider.SetUserInfoLoading(false);

                localStorage.RemoveAuthorizationTokens();
            }
            else
            {
                currentUserProvider.SetCurrentUserInfoFromResponse(response);
            }
        }

        public async Task<bool> GetRefreshedToken()
        {
            if (localStorage.HasRefreshToken())
            {
                var refreshToken = localStorage.GetRefreshToken();

                var requestHeaders = RequestHeadersUtility.GetRequestHeaders(localStorage);

                var response = await userServiceClient.GetRefreshedTokenAsync(new GetRefreshedTokenRequest { RefreshToken = refreshToken }, requestHeaders);

                if (!response.IsError)
                {
                    localStorage.SetAuthorizationTokens(response.TokenType, response.AccessToken, response.RefreshToken);
                }
                else
                {
                    localStorage.RemoveAuthorizationTokens();
                }

                return !response.IsError;
            }

            return false;
        }

        public async Task SignOut()
        {
            currentUserProvider.SetUserInfoLoading(true);

            try
            {
                if (localStorage.HasRefreshToken())
                {
                    var refreshToken = localStorage.GetRefreshToken();

                    var requestHeaders = RequestHeadersUtility.GetRequestHeaders(localStorage);

                    await userServiceClient.SignOutAsync(new SignOutRequest { RefreshToken = refreshToken }, requestHeaders);
                }
            }
            finally
            {
                localStorage.RemoveAuthorizationTokens();

                currentUserProvider.SetCurrentUserInfo(null);
            }
        }

        private async Task<GetCurrentUserInfoResponse> GetCurrentUserInfo()
        {
            try
            {
                var requestHeaders = RequestHeadersUtility.GetRequestHeaders(localStorage);

                return await userServiceClient.GetCurrentUserInfoAsync(new GetCurrentUserInfoRequest(), requestHeaders);
            }
            catch
            {
                return null;
            }
        }
    }
}
