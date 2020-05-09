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

        public async Task<ServiceResponse> CreateCarBrand(CarBrandFormModel formModel)
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

            return await carBrandServiceClient.CreateCarBrandAsync(request, headers);
        }

        public async Task<ServiceResponse> UpdateCarBrand(long carBrandId, CarBrandFormModel formModel)
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

            return await carBrandServiceClient.UpdateCarBrandAsync(request, headers);
        }

        public async Task<ServiceResponse> DeleteCarBrand(long carBrandId)
        {
            var request = new DeleteCarBrandRequest
            {
                Id = carBrandId
            };

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await carBrandServiceClient.DeleteCarBrandAsync(request, headers);
        }
    }
}
