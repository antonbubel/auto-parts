namespace AutoParts.Web.Server.Services
{
    using Grpc.Core;

    using MediatR;

    using FluentValidation;

    using Microsoft.AspNetCore.Authorization;

    using System.Linq;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.CarModels.Requests;
    using Core.Contracts.CarModels.Exceptions;
    using Core.Contracts.CarModels.Notifications;

    public class CarModelService : GrpcCarModelService.GrpcCarModelServiceBase
    {
        private readonly IMediator mediator;

        public CarModelService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<GetCarModelsResponse> GetCarModels(GetCarModelsRequest request, ServerCallContext context)
        {
            var carModels = await mediator.Send(new GetCarModelsByBrandRequest { CarBrandId = request.CarBrandId });

            var response = new GetCarModelsResponse();

            var responseCarModels = carModels
                .Select(carModel =>
                    new CarModel
                    {
                        Id = carModel.Id,
                        Name = carModel.Name,
                        ImageUrl = carModel.ImageUrl ?? string.Empty,
                        CarBrandId = carModel.CarBrandId,
                        CarBrandName = carModel.CarBrandName
                    })
                .ToArray();

            response.CarModels.AddRange(responseCarModels);

            return response;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<CreateCarModelResponse> CreateCarModel(CreateCarModelRequest request, ServerCallContext context)
        {
            var notification = new CreateCarModelNotification
            {
                CarBrandId = request.CarBrandId,
                Name = request.Name,
                ImageFileName = request.ImageName,
                ImageFileBuffer = request.Image.ToByteArray()
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return new CreateCarModelResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }
            catch (CreateCarModelException exception)
            {
                return new CreateCarModelResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new CreateCarModelResponse
            {
                IsError = false
            };
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<UpdateCarModelResponse> UpdateCarModel(UpdateCarModelRequest request, ServerCallContext context)
        {
            var notification = new UpdateCarModelNotification
            {
                CarModelId = request.Id,
                Name = request.Name,
                ImageFileName = request.ImageName,
                ImageFileBuffer = request.Image.ToByteArray()
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return new UpdateCarModelResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }
            catch (UpdateCarModelException exception)
            {
                return new UpdateCarModelResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new UpdateCarModelResponse
            {
                IsError = false
            };
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<DeleteCarModelResponse> DeleteCarModel(DeleteCarModelRequest request, ServerCallContext context)
        {
            var notification = new DeleteCarModelNotification
            {
                CarModelId = request.Id
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (DeleteCarModelException exception)
            {
                return new DeleteCarModelResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new DeleteCarModelResponse
            {
                IsError = false
            };
        }
    }
}
