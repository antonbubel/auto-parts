using AutoParts.Web.Client.Public.User.Models;
using AutoParts.Web.Protos;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace AutoParts.Web.Client.Public.User.Services
{
    public class UserSignUpService
    {
        private readonly GrpcSignUpService.GrpcSignUpServiceClient signUpServiceClient;
        private readonly GrpcSupplierService.GrpcSupplierServiceClient supplierServiceClient;

        public UserSignUpService(GrpcChannel channel)
        {
            signUpServiceClient = new GrpcSignUpService.GrpcSignUpServiceClient(channel);
            supplierServiceClient = new GrpcSupplierService.GrpcSupplierServiceClient(channel);
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

            var response = await signUpServiceClient.UserSignUpAsync(request);

            return !response.IsError;
        }

        public async Task<bool> SupplierSignUp(SupplierSignUpFormModel form)
        {
            var request = new SupplierSignUpRequest
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                InvitationToken = form.InvitationToken,
                OrganizationName = form.OrganizationName,
                OrganizationAddress = form.OrganizationAddress,
                Password = form.Password,
                PasswordConfirmation = form.PasswordConfirmation,
                PhoneNumber = form.PhoneNumber,
                Website = form.Website
            };

            var response = await signUpServiceClient.SupplierSignUpAsync(request);

            return !response.IsError;
        }

        public async Task<GetSupplierEmailFromInvitationResponse> GetSupplierEmailFromInvitation(string invitationToken)
        {
            var request = new GetSupplierEmailFromInvitationRequest
            {
                InvitationToken = invitationToken
            };

            return await supplierServiceClient.GetSupplierEmailFromInvitationAsync(request);
        }
    }
}
