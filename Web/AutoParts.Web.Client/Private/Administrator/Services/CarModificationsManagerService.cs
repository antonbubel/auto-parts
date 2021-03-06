﻿namespace AutoParts.Web.Client.Private.Administrator.Services
{
    using Grpc.Net.Client;

    using Blazored.LocalStorage;

    using System.Threading.Tasks;

    using Protos;

    using Models;

    using Shared.Utils;

    public class CarModificationsManagerService
    {
        private readonly ISyncLocalStorageService localStorage;
        private readonly GrpcCarModificationService.GrpcCarModificationServiceClient carModificationServiceClient;

        public CarModificationsManagerService(ISyncLocalStorageService localStorage, GrpcChannel channel)
        {
            carModificationServiceClient = new GrpcCarModificationService.GrpcCarModificationServiceClient(channel);
            this.localStorage = localStorage;
        }

        public async Task<ServiceResponse> CreateCarModification(CarModificationFormModel formModel)
        {
            var request = new CreateCarModificationRequest
            {
                CarModelId = formModel.CarModelId,
                Name = formModel.Name,
                Description = formModel.Description,
                Year = int.Parse(formModel.Year)
            };

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await carModificationServiceClient.CreateCarModificationAsync(request, headers);
        }

        public async Task<ServiceResponse> UpdateCarModification(long carModificationId, CarModificationFormModel formModel)
        {
            var request = new UpdateCarModificationRequest
            {
                Id = carModificationId,
                Name = formModel.Name,
                Description = formModel.Description,
                Year = int.Parse(formModel.Year)
            };

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await carModificationServiceClient.UpdateCarModificationAsync(request, headers);
        }

        public async Task<ServiceResponse> DeleteCarModification(long carModificationId)
        {
            var request = new DeleteCarModificationRequest
            {
                Id = carModificationId
            };

            var headers = RequestHeadersUtility.GetRequestHeaders(localStorage);

            return await carModificationServiceClient.DeleteCarModificationAsync(request, headers);
        }
    }
}
