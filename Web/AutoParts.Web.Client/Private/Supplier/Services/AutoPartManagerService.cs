namespace AutoParts.Web.Client.Private.Supplier.Services
{
    using Grpc.Net.Client;

    using Google.Protobuf;

    using Blazored.LocalStorage;

    using System.Threading.Tasks;

    using Protos;

    using Models;

    using Shared.Utils;

    public class AutoPartManagerService
    {
        private readonly ISyncLocalStorageService localStorage;
        private readonly GrpcAutoPartService.GrpcAutoPartServiceClient autoPartServiceClient;

        public AutoPartManagerService(ISyncLocalStorageService localStorage, GrpcChannel channel)
        {
            this.localStorage = localStorage;
            autoPartServiceClient = new GrpcAutoPartService.GrpcAutoPartServiceClient(channel);
        }

        public async Task<ServiceResponse> CreateAutoPart(AutoPartFormModel formModel)
        {
            var request = CreateGetAutoPartRequestFromFormModel(formModel);

            if (formModel.ImageFileInfo != null && formModel.ImageFileBuffer != null)
            {
                request.ImageFileName = formModel.ImageFileInfo.Name;
                request.ImageFileBuffer = ByteString.CopyFrom(formModel.ImageFileBuffer);
            }

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await autoPartServiceClient.CreateAutoPartAsync(request, headers);
        }

        private CreateAutoPartRequest CreateGetAutoPartRequestFromFormModel(AutoPartFormModel formModel)
        {
            return new CreateAutoPartRequest
            {
                Name = formModel.Name,
                Description = formModel.Description,
                ImageFileName = string.Empty,
                ImageFileBuffer = ByteString.Empty,
                Price = formModel.Price,
                Quantity = formModel.Quantity,
                CountryId = formModel.CountryId,
                CarModificationId = formModel.CarModificationId,
                ManufacturerId = formModel.ManufacturerId,
                SubCatalogId = formModel.SubCatalogId
            };
        }
    }
}
