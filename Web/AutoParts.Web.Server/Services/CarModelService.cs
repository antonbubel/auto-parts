namespace AutoParts.Web.Server.Services
{
    using AutoMapper;

    using Grpc.Core;

    using MediatR;

    using FluentValidation;

    using Microsoft.AspNetCore.Authorization;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.CarModels.Requests;
    using Core.Contracts.CarModels.Exceptions;
    using Core.Contracts.CarModels.Notifications;

    public class CarModelService : GrpcCarModelService.GrpcCarModelServiceBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public CarModelService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public override async Task<GetCarModelResponse> GetCarModel(GetCarModelRequest request, ServerCallContext context)
        {
            var carModel = await mediator.Send(new GetCarModelByIdRequest { CarModelId = request.Id });

            if (carModel == null)
            {
                return new GetCarModelResponse
                {
                    Status = ResponseStatus.NotFound
                };
            }

            return new GetCarModelResponse
            {
                Model = mapper.Map<CarModel>(carModel),
                Status = ResponseStatus.Ok
            };
        }

        [AllowAnonymous]
        public override async Task<GetCarModelsResponse> GetCarModels(GetCarModelsRequest request, ServerCallContext context)
        {
            var carModels = await mediator.Send(new GetCarModelsByBrandRequest { CarBrandId = request.CarBrandId });

            var response = new GetCarModelsResponse();

            mapper.Map(carModels, response.CarModels);

            return response;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> CreateCarModel(CreateCarModelRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<CreateCarModelNotification>(request);

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (CreateCarModelException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> UpdateCarModel(UpdateCarModelRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<UpdateCarModelNotification>(request);

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (UpdateCarModelException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> DeleteCarModel(DeleteCarModelRequest request, ServerCallContext context)
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
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }
    }
}
