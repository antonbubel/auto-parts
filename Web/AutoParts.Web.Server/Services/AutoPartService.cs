namespace AutoParts.Web.Server.Services
{
    using AutoMapper;

    using FluentValidation;

    using MediatR;

    using Grpc.Core;

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    using Protos;

    using Core.Contracts.AutoParts.Exceptions;
    using Core.Contracts.AutoParts.Notifications;
    using GetAutoPartsRequestBL = Core.Contracts.AutoParts.Requests.GetAutoPartsRequest;

    using Infrastructure.Exceptions;

    public class AutoPartService : GrpcAutoPartService.GrpcAutoPartServiceBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public AutoPartService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public override async Task<GetAutoPartsResponse> GetAutoParts(GetAutoPartsRequest request, ServerCallContext context)
        {
            var mediatorRequest = mapper.Map<GetAutoPartsRequestBL>(request);
            var pageModel = await mediator.Send(mediatorRequest);

            var response = new GetAutoPartsResponse
            {
                TotalNumberOfItems = pageModel.TotalNumberOfItems
            };

            mapper.Map(pageModel.Items, response.AutoParts);

            return response;
        }

        [Authorize(nameof(UserType.Supplier))]
        public override async Task<ServiceResponse> CreateAutoPart(CreateAutoPartRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<CreateAutoPartNotification>(request);

            notification.SupplierId = context.GetLoggedInUserId().Value;

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (CreateAutoPartException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }

        [Authorize(nameof(UserType.Supplier))]
        public override async Task<ServiceResponse> UpdateAutoPart(UpdateAutoPartRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<UpdateAutoPartNotification>(request);

            notification.SupplierId = context.GetLoggedInUserId().Value;

            try
            {
                await mediator.Publish(notification);
            }
            catch (ForbiddenException)
            {
                return ServiceResponseBuilder.Forbidden;
            }
            catch (NotFoundException)
            {
                return ServiceResponseBuilder.NotFound;
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (UpdateAutoPartException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }

        [Authorize(nameof(UserType.Supplier))]
        public override async Task<ServiceResponse> DeleteAutoPart(DeleteAutoPartRequest request, ServerCallContext context)
        {
            var notification = new DeleteAutoPartNotification
            {
                AutoPartId = request.AutoPartId,
                SupplierId = context.GetLoggedInUserId().Value
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (ForbiddenException)
            {
                return ServiceResponseBuilder.Forbidden;
            }
            catch (NotFoundException)
            {
                return ServiceResponseBuilder.NotFound;
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (DeleteAutoPartException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }
    }
}
