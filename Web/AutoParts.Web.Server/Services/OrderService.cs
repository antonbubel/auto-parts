namespace AutoParts.Web.Server.Services
{
    using AutoMapper;

    using MediatR;

    using FluentValidation;

    using Grpc.Core;

    using Microsoft.AspNetCore.Authorization;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.Orders.Exceptions;
    using Core.Contracts.Orders.Notifications;

    public class OrderService : GrpcOrderService.GrpcOrderServiceBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public OrderService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [AllowAnonymous]
        public override async Task<ServiceResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<CreateOrderNotification>(request);

            notification.UserId = context.GetLoggedInUserId();
            
            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return ServiceResponseBuilder.FromValidationException(exception);
            }
            catch (CreateOrderException exception)
            {
                return ServiceResponseBuilder.FromApiException(exception);
            }

            return ServiceResponseBuilder.Ok;
        }
    }
}
