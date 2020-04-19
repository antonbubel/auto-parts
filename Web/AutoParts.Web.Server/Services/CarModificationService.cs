namespace AutoParts.Web.Server.Services
{
    using Grpc.Core;

    using MediatR;

    using FluentValidation;

    using Microsoft.AspNetCore.Authorization;

    using System.Linq;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.CarModifications.Requests;
    using Core.Contracts.CarModifications.Exceptions;
    using Core.Contracts.CarModifications.Notifications;

    public class CarModificationService : GrpcCarModificationService.GrpcCarModificationServiceBase
    {
        private readonly IMediator mediator;

        public CarModificationService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<GetCarModificationsResponse> GetCarModifications(GetCarModificationsRequest request, ServerCallContext context)
        {
            var mediatorRequest = new GetCarModificationsByModelRequest
            {
                CarModelId = request.CarModelId,
                Year = request.Year == default ? null : (int?)request.Year
            };

            var carModifications = await mediator.Send(mediatorRequest);

            var response = new GetCarModificationsResponse();

            var responseCarModifications = carModifications
                .Select(carModification =>
                    new CarModification
                    {
                        Id = carModification.Id,
                        Name = carModification.Name,
                        Description = carModification.Description ?? string.Empty,
                        Year = carModification.Year
                    })
                .ToArray();

            response.CarModifications.AddRange(responseCarModifications);

            return response;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<CreateCarModificationResponse> CreateCarModification(CreateCarModificationRequest request, ServerCallContext context)
        {
            var notification = new CreateCarModificationNotification
            {
                CarModelId = request.CarModelId,
                Name = request.Name,
                Description = request.Description,
                Year = request.Year
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return new CreateCarModificationResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }
            catch (CreateCarModificationException exception)
            {
                return new CreateCarModificationResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new CreateCarModificationResponse
            {
                IsError = false
            };
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<UpdateCarModificationResponse> UpdateCarModification(UpdateCarModificationRequest request, ServerCallContext context)
        {
            var notification = new UpdateCarModificationNotification
            {
                CarModificationId = request.Id,
                Name = request.Name,
                Description = request.Description,
                Year = request.Year
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return new UpdateCarModificationResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }
            catch (UpdateCarModificationException exception)
            {
                return new UpdateCarModificationResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new UpdateCarModificationResponse
            {
                IsError = false
            };
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<DeleteCarModificationResponse> DeleteCarModification(DeleteCarModificationRequest request, ServerCallContext context)
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
                return new DeleteCarModificationResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new DeleteCarModificationResponse
            {
                IsError = false
            };
        }
    }
}
