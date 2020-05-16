namespace AutoParts.Web.Client.Private.Administrator.Services
{
    using Blazored.LocalStorage;

    using Google.Protobuf;
    using Grpc.Net.Client;

    using System.Threading.Tasks;

    using Protos;
    using Models;
    using Shared.Utils;

    public class ManufacturerManagerService
    {
        private readonly ISyncLocalStorageService localStorage;
        private readonly GrpcManufacturerService.GrpcManufacturerServiceClient manufacturerServiceClient;

        public ManufacturerManagerService(ISyncLocalStorageService localStorage, GrpcChannel channel)
        {
            this.localStorage = localStorage;
            manufacturerServiceClient = new GrpcManufacturerService.GrpcManufacturerServiceClient(channel);
        }

        public async Task<ServiceResponse> CreateManufacturer(ManufacturerFormModel formModel)
        {
            var request = new CreateManufacturerRequest
            {
                CountryId = formModel.CountryId,
                Name = formModel.Name,
                Description = formModel.Description,
                ImageFileName = string.Empty,
                ImageFileBuffer = ByteString.Empty
            };

            if (formModel.ImageFileInfo != null && formModel.ImageBuffer != null)
            {
                request.ImageFileName = formModel.ImageFileInfo.Name;
                request.ImageFileBuffer = ByteString.CopyFrom(formModel.ImageBuffer);
            }

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await manufacturerServiceClient.CreateManufacturerAsync(request, headers);
        }

        public async Task<ServiceResponse> UpdateManufacturer(long manufacturerId, ManufacturerFormModel formModel)
        {
            var request = new UpdateManufacturerRequest
            {
                Id = manufacturerId,
                CountryId = formModel.CountryId,
                Name = formModel.Name,
                Description = formModel.Description,
                ImageFileName = string.Empty,
                ImageFileBuffer = ByteString.Empty
            };

            if (formModel.ImageFileInfo != null && formModel.ImageBuffer != null)
            {
                request.ImageFileName = formModel.ImageFileInfo.Name;
                request.ImageFileBuffer = ByteString.CopyFrom(formModel.ImageBuffer);
            }

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await manufacturerServiceClient.UpdateManufacturerAsync(request, headers);
        }

        public async Task<ServiceResponse> DeleteManufacturer(long manufacturerId)
        {
            var request = new DeleteManufacturerRequest
            {
                Id = manufacturerId
            };

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await manufacturerServiceClient.DeleteManufacturerAsync(request, headers);
        }
    }
}
