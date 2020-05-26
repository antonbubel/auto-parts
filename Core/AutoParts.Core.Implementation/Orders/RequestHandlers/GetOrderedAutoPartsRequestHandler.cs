namespace AutoParts.Core.Implementation.Orders.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Orders.Models;
    using Contracts.Orders.Requests;

    using Contracts.Files.Requests;

    using Data.Model.Enums;
    using Data.Model.Projections;
    using Data.Model.Repositories;

    using Infrastructure.Exceptions;

    public class GetOrderedAutoPartsRequestHandler : IRequestHandler<GetOrderedAutoPartsRequest, OrderAutoPartModel[]>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IUserRepository userRepository;

        public GetOrderedAutoPartsRequestHandler(
            IMapper mapper,
            IMediator mediator,
            IOrderItemRepository orderItemRepository,
            IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.orderItemRepository = orderItemRepository;
            this.userRepository = userRepository;
        }

        public async Task<OrderAutoPartModel[]> Handle(GetOrderedAutoPartsRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetOrderedAutoPartsRequest)} argument cannot be null.");
            }

            var user = await userRepository.FindAsync(request.UserId)
                .ConfigureAwait(false);

            if (user.UserTypeId == UserType.User)
            {
                return await GetUserOrderedAutoParts(request);
            }

            if (user.UserTypeId == UserType.Supplier)
            {
                return await GetSupplierOrderedAutoParts(request);
            }

            throw new ForbiddenException();
        }

        private async Task<OrderAutoPartModel[]> GetUserOrderedAutoParts(GetOrderedAutoPartsRequest request)
        {
            var orderItems = await orderItemRepository.GetOrderItems(request.OrderId)
                .ConfigureAwait(false);

            return await MapOrderItems(orderItems);
        }

        private async Task<OrderAutoPartModel[]> GetSupplierOrderedAutoParts(GetOrderedAutoPartsRequest request)
        {
            var orderItems = await orderItemRepository.GetOrderItems(request.OrderId, request.UserId)
                .ConfigureAwait(false);

            return await MapOrderItems(orderItems);
        }

        private async Task<OrderAutoPartModel[]> MapOrderItems(OrderItemProjection[] orderItems)
        {
            var orderAutoParts = mapper.Map<OrderAutoPartModel[]>(orderItems);

            foreach (var orderAutoPart in orderAutoParts)
            {
                if (!string.IsNullOrEmpty(orderAutoPart.ImageUrl))
                {
                    orderAutoPart.ImageUrl = await mediator.Send(new GetFileUrlRequest { FileName = orderAutoPart.ImageUrl });
                }
            }

            return orderAutoParts;
        }
    }
}
