namespace AutoParts.Web.Client.Private.Supplier.Services
{
    using Blazored.LocalStorage;

    using Grpc.Net.Client;

    using Google.Protobuf;

    using System.Threading.Tasks;

    using Protos;

    using Models;

    using Shared.Utils;

    public class PrivateSupplierService
    {
        private readonly ISyncLocalStorageService localStorage;
        private readonly GrpcSupplierService.GrpcSupplierServiceClient supplierServiceClient;

        public PrivateSupplierService(GrpcChannel channel, ISyncLocalStorageService localStorage)
        {
            supplierServiceClient = new GrpcSupplierService.GrpcSupplierServiceClient(channel);
            this.localStorage = localStorage;
        }

        public async Task<SupplierPrivateProfile> GetSupplierProfile()
        {
            var request = new GetCurrentUserSupplierPrivateProfileRequest();
            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);
            var response = await supplierServiceClient.GetCurrentUserSupplierPrivateProfileAsync(request, headers);

            return response.Model;
        }

        public async Task<ServiceResponse> UpdateSupplierProfile(SupplierProfileFormModel formModel)
        {
            var request = new UpdateSupplierProfileRequest
            {
                Name = formModel.OrganizationName,
                OrganizationAddress = formModel.OrganizationAddress,
                OrganizationDescription = formModel.OrganizationDescription,
                Website = formModel.Website,
                SalesEmail = formModel.SalesEmail,
                SalesPhoneNumber = formModel.SalesPhoneNumber,
                LogoFileName = string.Empty,
                LogoFileBuffer = ByteString.Empty
            };

            if (formModel.LogoFileInfo != null && formModel.LogoBuffer != null)
            {
                request.LogoFileName = formModel.LogoFileInfo.Name;
                request.LogoFileBuffer = ByteString.CopyFrom(formModel.LogoBuffer);
            }

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await supplierServiceClient.UpdateSupplierProfileAsync(request, headers);
        }
    }
}
