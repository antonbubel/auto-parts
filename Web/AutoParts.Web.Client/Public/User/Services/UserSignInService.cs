using AutoParts.Web.Client.Public.User.Models;
using AutoParts.Web.Client.Shared.Services;
using AutoParts.Web.Protos;
using Blazored.LocalStorage;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace AutoParts.Web.Client.Public.User.Services
{
    using Shared.Utils;
    using System;

    public class UserSignInService
    {
        private readonly GrpcSignInService.GrpcSignInServiceClient signInClient;
        private readonly ISyncLocalStorageService localStorage;
        private readonly CurrentUserService currentUserService;

        public UserSignInService(GrpcChannel channel, ISyncLocalStorageService localStorage, CurrentUserService currentUserService)
        {
            signInClient = new GrpcSignInService.GrpcSignInServiceClient(channel);
            this.localStorage = localStorage;
            this.currentUserService = currentUserService;
        }

        public async Task<bool> SignIn(UserSignInFormModel form)
        {
            var request = new UserSignInRequest
            {
                Email = form.Email,
                Password = form.Password
            };

            var response = await signInClient.SignInAsync(request);

            localStorage.SetAuthorizationTokens(response.TokenType, response.AccessToken, response.RefreshToken);

            try
            {
                await currentUserService.SyncCurrentUserInfo();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
            }

            return !response.IsError;
        }
    }
}
