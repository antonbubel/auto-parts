namespace AutoParts.Web.Client.Private.Administrator.Services
{
    using Grpc.Net.Client;

    using Blazored.LocalStorage;

    using System.Threading.Tasks;

    using Protos;

    using Models;

    using Shared.Utils;

    public class SuppliersManagerService
    {
        private readonly ISyncLocalStorageService localStorage;
        private readonly GrpcSupplierService.GrpcSupplierServiceClient supplierServiceClient;

        public SuppliersManagerService(ISyncLocalStorageService localStorage, GrpcChannel channel)
        {
            this.localStorage = localStorage;
            supplierServiceClient = new GrpcSupplierService.GrpcSupplierServiceClient(channel);
        }

        public async Task<InviteSupplierResponse> InviteSupplier(InviteSupplierFormModel form)
        {
            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await supplierServiceClient.InviteSupplierAsync(new InviteSupplierRequest { Email = form.Email, Name = form.Name }, headers);
        }
    }
}
