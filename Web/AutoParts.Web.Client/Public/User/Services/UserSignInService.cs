using AutoParts.Web.Client.Public.User.Models;
using AutoParts.Web.Client.Shared.Constants;
using AutoParts.Web.Protos;
using Blazored.LocalStorage;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace AutoParts.Web.Client.Public.User.Services
{
    public class UserSignInService
    {
        private readonly GrpcSignInService.GrpcSignInServiceClient signInClient;
        private readonly ISyncLocalStorageService localStorage;

        public UserSignInService(GrpcChannel channel, ISyncLocalStorageService localStorage)
        {
            signInClient = new GrpcSignInService.GrpcSignInServiceClient(channel);
            this.localStorage = localStorage;
        }

        public async Task<bool> SignIn(UserSignInFormModel form)
        {
            var request = new UserSignInRequest
            {
                Email = form.Email,
                Password = form.Password
            };

            var response = await signInClient.SignInAsync(request);

            localStorage.SetItem(LocalStorageConstants.TokenType, response.TokenType);
            localStorage.SetItem(LocalStorageConstants.AccessToken, response.AccessToken);

            return response.IsError;
        }
    }
}
