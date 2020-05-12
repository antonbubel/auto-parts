namespace AutoParts.Web.Server.Services
{
    using MediatR;

    using AutoMapper;

    using FluentValidation;

    using Grpc.Core;

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    using Core.Contracts.Manufacturer.Requests;
    using Core.Contracts.Manufacturer.Exceptions;
    using Core.Contracts.Manufacturer.Notifications;

    using Protos;

    using Infrastructure.Exceptions;

    public class ManufacturerService : GrpcManufacturerService.GrpcManufacturerServiceBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ManufacturerService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public override async Task<GetManufacturersResponse> GetManufacturers(GetManufacturersRequest request, ServerCallContext context)
        {
            var manufacturers = await mediator.Send(new GetManufacturersByCountryRequest { CountryId = request.CountryId });
            var response = new GetManufacturersResponse();

            mapper.Map(manufacturers, response.Manufacturers);

            return response;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> CreateManufacturer(CreateManufacturerRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<CreateManufacturerNotification>(request);
            
            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (CreateManufacturerException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> UpdateManufacturer(UpdateManufacturerRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<UpdateManufacturerNotification>(request);

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (NotFoundException)
            {
                return ServiceResponseBuilder.NotFound;
            }
            catch (UpdateManufacturerException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> DeleteManufacturer(DeleteManufacturerRequest request, ServerCallContext context)
        {
            var notification = new DeleteManufacturerNotification
            {
                ManufacturerId = request.Id
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (NotFoundException)
            {
                return ServiceResponseBuilder.NotFound;
            }
            catch (DeleteManufacturerException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }
    }
}
