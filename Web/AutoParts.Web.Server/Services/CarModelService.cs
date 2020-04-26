namespace AutoParts.Web.Server.Services
{
    using AutoMapper;

    using Grpc.Core;

    using MediatR;

    using FluentValidation;

    using Microsoft.AspNetCore.Authorization;

    using System.Linq;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.CarModels.Models;
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
                    Status = RequestStatus.NotFound
                };
            }

            return new GetCarModelResponse
            {
                Model = mapper.Map<CarModel>(carModel),
                Status = RequestStatus.Ok
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
        public override async Task<CreateCarModelResponse> CreateCarModel(CreateCarModelRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<CreateCarModelNotification>(request);

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
            var notification = mapper.Map<UpdateCarModelNotification>(request);

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
