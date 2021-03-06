﻿namespace AutoParts.Web.Client.Private.Administrator.Services
{
    using Google.Protobuf;

    using Grpc.Net.Client;

    using Blazored.LocalStorage;

    using System.Threading.Tasks;

    using Protos;

    using Models;

    using Shared.Utils;

    public class CarModelsManagerService
    {
        private readonly ISyncLocalStorageService localStorage;
        private readonly GrpcCarModelService.GrpcCarModelServiceClient carModelServiceClient;

        public CarModelsManagerService(ISyncLocalStorageService localStorage, GrpcChannel channel)
        {
            carModelServiceClient = new GrpcCarModelService.GrpcCarModelServiceClient(channel);
            this.localStorage = localStorage;
        }

        public async Task<ServiceResponse> CreateCarModel(CarModelFormModel formModel)
        {
            var request = new CreateCarModelRequest
            {
                CarBrandId = formModel.CarBrandId,
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

            return await carModelServiceClient.CreateCarModelAsync(request, headers);
        }

        public async Task<ServiceResponse> UpdateCarModel(long carModelId, CarBrandFormModel formModel)
        {
            var request = new UpdateCarModelRequest
            {
                Id = carModelId,
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

            return await carModelServiceClient.UpdateCarModelAsync(request, headers);
        }

        public async Task<ServiceResponse> DeleteCarModel(long carModelId)
        {
            var request = new DeleteCarModelRequest
            {
                Id = carModelId
            };

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await carModelServiceClient.DeleteCarModelAsync(request, headers);
        }
    }
}
