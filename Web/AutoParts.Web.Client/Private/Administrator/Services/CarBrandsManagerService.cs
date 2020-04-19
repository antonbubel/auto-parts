namespace AutoParts.Web.Client.Private.Administrator.Services
{
    using Google.Protobuf;

    using Grpc.Net.Client;

    using Blazored.LocalStorage;

    using System.Threading.Tasks;

    using Protos;

    using Models;

    using Shared.Utils;

    public class CarBrandsManagerService
    {
        private readonly ISyncLocalStorageService localStorage;
        private readonly GrpcCarBrandService.GrpcCarBrandServiceClient carBrandServiceClient;

        public CarBrandsManagerService(ISyncLocalStorageService localStorage, GrpcChannel channel)
        {
            carBrandServiceClient = new GrpcCarBrandService.GrpcCarBrandServiceClient(channel);
            this.localStorage = localStorage;
        }

        public async Task<bool> CreateCarBrand(CarBrandFormModel formModel)
        {
            var request = new CreateCarBrandRequest
            {
                Name = formModel.Name,
                ImageName = string.Empty,
                Image = ByteString.Empty
            };

            if (formModel.ImageFileInfo != null && formModel.ImageBuffer != null)
            {
                request.ImageName = formModel.ImageFileInfo.Name;
                request.Image = ByteString.CopyFrom(formModel.ImageBuffer);
            }

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            var response = await carBrandServiceClient.CreateCarBrandAsync(request, headers);

            return !response.IsError;
        }

        public async Task<bool> UpdateCarBrand(long carBrandId, CarBrandFormModel formModel)
        {
            var request = new UpdateCarBrandRequest
            {
                Id = carBrandId,
                Name = formModel.Name,
                ImageName = string.Empty,
                Image = ByteString.Empty
            };

            if (formModel.ImageFileInfo != null && formModel.ImageBuffer != null)
            {
                request.ImageName = formModel.ImageFileInfo.Name;
                request.Image = ByteString.CopyFrom(formModel.ImageBuffer);
            }

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            var response = await carBrandServiceClient.UpdateCarBrandAsync(request, headers);

            return !response.IsError;
        }

        public async Task<bool> DeleteCarBrand(long carBrandId)
        {
            var request = new DeleteCarBrandRequest
            {
                Id = carBrandId
            };

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            var response = await carBrandServiceClient.DeleteCarBrandAsync(request, headers);

            return !response.IsError;
        }
    }
}
