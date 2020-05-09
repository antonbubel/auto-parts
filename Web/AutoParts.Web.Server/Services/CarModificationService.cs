namespace AutoParts.Web.Server.Services
{
    using AutoMapper;

    using Grpc.Core;

    using MediatR;

    using FluentValidation;

    using Microsoft.AspNetCore.Authorization;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.CarModifications.Requests;
    using Core.Contracts.CarModifications.Exceptions;
    using Core.Contracts.CarModifications.Notifications;

    public class CarModificationService : GrpcCarModificationService.GrpcCarModificationServiceBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public CarModificationService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public override async Task<GetCarModificationsResponse> GetCarModifications(GetCarModificationsRequest request, ServerCallContext context)
        {
            var mediatorRequest = new GetCarModificationsByModelRequest
            {
                CarModelId = request.CarModelId,
                Year = request.Year == default ? null : (int?)request.Year
            };

            var carModifications = await mediator.Send(mediatorRequest);

            var response = new GetCarModificationsResponse();

            mapper.Map(carModifications, response.CarModifications);

            return response;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> CreateCarModification(CreateCarModificationRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<CreateCarModificationNotification>(request);

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (CreateCarModificationException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> UpdateCarModification(UpdateCarModificationRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<UpdateCarModificationNotification>(request);

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (UpdateCarModificationException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> DeleteCarModification(DeleteCarModificationRequest request, ServerCallContext context)
        {
            var notification = new DeleteCarModificationNotification
            {
                CarModificationId = request.Id
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (DeleteCarModificationException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }
    }
}
