using AutoParts.Web.Client.Public.User.Models;
using AutoParts.Web.Protos;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace AutoParts.Web.Client.Public.User.Services
{
    public class UserSignUpService
    {
        private readonly GrpcSignUpService.GrpcSignUpServiceClient signUpClient;

        public UserSignUpService(GrpcChannel channel)
        {
            signUpClient = new GrpcSignUpService.GrpcSignUpServiceClient(channel);
        }

        public async Task<bool> SignUp(UserSignUpFormModel form)
        {
            var request = new UserSignUpRequest
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                Password = form.Password,
                PasswordConfirmation = form.PasswordConfirmation
            };

            var response = await signUpClient.UserSignUpAsync(request);

            return !response.IsError;
        }
    }
}
