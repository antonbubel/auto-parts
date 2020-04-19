namespace AutoParts.Web.Server.Services
{
    using Grpc.Core;

    using MediatR;

    using FluentValidation;

    using Microsoft.AspNetCore.Authorization;

    using System.Linq;
    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.CarBrands.Exceptions;
    using Core.Contracts.CarBrands.Notifications;

    using GetCarBrandsRequest = Core.Contracts.CarBrands.Requests.GetCarBrandsRequest;

    public class CarBrandService : GrpcCarBrandService.GrpcCarBrandServiceBase
    {
        private readonly IMediator mediator;

        public CarBrandService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<GetCarBrandsResponse> GetCarBrands(Protos.GetCarBrandsRequest request, ServerCallContext context)
        {
            var carBrands = await mediator.Send(new GetCarBrandsRequest());

            var response = new GetCarBrandsResponse();

            var responseCarBrands = carBrands
                .Select(carBrand =>
                    new CarBrand
                    {
                        Id = carBrand.Id,
                        Name = carBrand.Name,
                        ImageUrl = carBrand.ImageUrl ?? string.Empty
                    })
                .ToArray();

            response.CarBrands.AddRange(responseCarBrands);

            return response;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<CreateCarBrandResponse> CreateCarBrand(CreateCarBrandRequest request, ServerCallContext context)
        {
            var notification = new CreateCarBrandNotification
            {
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
                return new CreateCarBrandResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }
            catch (CreateCarBrandException exception)
            {
                return new CreateCarBrandResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new CreateCarBrandResponse
            {
                IsError = false
            };
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<UpdateCarBrandResponse> UpdateCarBrand(UpdateCarBrandRequest request, ServerCallContext context)
        {
            var notification = new UpdateCarBrandNotification
            {
                CarBrandId = request.Id,
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
                return new UpdateCarBrandResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }
            catch (UpdateCarBrandException exception)
            {
                return new UpdateCarBrandResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new UpdateCarBrandResponse
            {
                IsError = false
            };
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<DeleteCarBrandResponse> DeleteCarBrand(DeleteCarBrandRequest request, ServerCallContext context)
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
                return new DeleteCarBrandResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new DeleteCarBrandResponse
            {
                IsError = false
            };
        }
    }
}
