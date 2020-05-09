namespace AutoParts.Web.Server.Services
{
    using AutoMapper;
    
    using Grpc.Core;

    using MediatR;

    using FluentValidation;

    using Microsoft.AspNetCore.Authorization;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.CarBrands.Requests;
    using Core.Contracts.CarBrands.Exceptions;
    using Core.Contracts.CarBrands.Notifications;

    using GetCarBrandsRequest = Core.Contracts.CarBrands.Requests.GetCarBrandsRequest;

    public class CarBrandService : GrpcCarBrandService.GrpcCarBrandServiceBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public CarBrandService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public override async Task<GetCarBrandResponse> GetCarBrand(GetCarBrandRequest request, ServerCallContext context)
        {
            var carBrand = await mediator.Send(new GetCarBrandByIdRequest { CarBrandId = request.Id });

            if (carBrand == null)
            {
                return new GetCarBrandResponse
                {
                    Status = ResponseStatus.NotFound
                };
            }

            return new GetCarBrandResponse
            {
                Model = mapper.Map<CarBrand>(carBrand),
                Status = ResponseStatus.Ok
            };
        }

        [AllowAnonymous]
        public override async Task<GetCarBrandsResponse> GetCarBrands(Protos.GetCarBrandsRequest request, ServerCallContext context)
        {
            var carBrands = await mediator.Send(new GetCarBrandsRequest());

            var response = new GetCarBrandsResponse();

            mapper.Map(carBrands, response.CarBrands);

            return response;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> CreateCarBrand(CreateCarBrandRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<CreateCarBrandNotification>(request);

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (CreateCarBrandException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> UpdateCarBrand(UpdateCarBrandRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<UpdateCarBrandNotification>(request);

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (UpdateCarBrandException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<ServiceResponse> DeleteCarBrand(DeleteCarBrandRequest request, ServerCallContext context)
        {
            var notification = new DeleteCarBrandNotification
            {
                CarBrandId = request.Id
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (DeleteCarBrandException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }
    }
}
