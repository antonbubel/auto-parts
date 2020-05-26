namespace AutoParts.Web.Server.Services
{
    using AutoMapper;

    using MediatR;

    using FluentValidation;

    using Grpc.Core;

    using Microsoft.AspNetCore.Authorization;

    using System.Threading.Tasks;

    using Protos;

    using Core.Contracts.Orders.Models;
    using Core.Contracts.Orders.Requests;
    using Core.Contracts.Orders.Exceptions;
    using Core.Contracts.Orders.Notifications;

    using Infrastructure.Exceptions;

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

        public override async Task<GetOrderResponse> GetOrder(GetOrderRequest request, ServerCallContext context)
        {
            var mediatorRequest = new GetOrderByIdRequest
            {
                OrderId = request.OrderId,
                UserId = context.GetLoggedInUserId().Value
            };

            OrderModel order;

            try
            {
                order = await mediator.Send(mediatorRequest);
            }
            catch (NotFoundException)
            {
                return new GetOrderResponse
                {
                    Status = ResponseStatus.NotFound
                };
            }
            catch (ForbiddenException)
            {
                return new GetOrderResponse
                {
                    Status = ResponseStatus.Forbidden
                };
            }

            return new GetOrderResponse
            {
                Order = mapper.Map<Order>(order),
                Status = ResponseStatus.Ok
            };
        }

        [Authorize(nameof(Protos.UserType.User))]
        public override async Task<GetUserOrdersResponse> GetUserOrders(PaginationFilter request, ServerCallContext context)
        {
            var mediatorRequest = new GetUserOrdersRequest
            {
                PageSize = request.PageSize,
                PageNumber = request.PageNumber,
                UserId = context.GetLoggedInUserId().Value
            };

            var pageModel = await mediator.Send(mediatorRequest);

            var response = new GetUserOrdersResponse
            {
                TotalNumberOfItems = pageModel.TotalNumberOfItems
            };

            mapper.Map(pageModel.Items, response.Orders);

            return response;
        }

        [Authorize(nameof(UserType.Supplier))]
        public override async Task<GetSupplierOrdersResponse> GetSupplierOrders(PaginationFilter request, ServerCallContext context)
        {
            var mediatorRequest = new GetSupplierOrdersRequest
            {
                PageSize = request.PageSize,
                PageNumber = request.PageNumber,
                SupplierId = context.GetLoggedInUserId().Value
            };

            var pageModel = await mediator.Send(mediatorRequest);

            var response = new GetSupplierOrdersResponse
            {
                TotalNumberOfItems = pageModel.TotalNumberOfItems
            };

            mapper.Map(pageModel.Items, response.Orders);

            return response;
        }

        [Authorize]
        public override async Task<GetOrderItemsResponse> GetOrderItems(GetOrderItemsRequest request, ServerCallContext context)
        {
            var mediatorRequest = new GetOrderedAutoPartsRequest
            {
                OrderId = request.OrderId,
                UserId = context.GetLoggedInUserId().Value
            };

            var orderedAutoParts = await mediator.Send(mediatorRequest);

            var response = new GetOrderItemsResponse();

            mapper.Map(orderedAutoParts, response.Items);

            return response;
        }
    }
}
