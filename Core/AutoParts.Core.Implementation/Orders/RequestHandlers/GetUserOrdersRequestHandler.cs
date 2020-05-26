namespace AutoParts.Core.Implementation.Orders.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Common.Extensions;

    using Contracts.Common.Models;

    using Contracts.Orders.Models;
    using Contracts.Orders.Requests;

    using Data.Model.Repositories;

    public class GetUserOrdersRequestHandler : IRequestHandler<GetUserOrdersRequest, PageModel<OrderModel>>
    {
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;

        public GetUserOrdersRequestHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            this.mapper = mapper;
            this.orderRepository = orderRepository;
        }

        public async Task<PageModel<OrderModel>> Handle(GetUserOrdersRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetUserOrdersRequest)} argument cannot be null.");
            }

            var itemsToSkip = request.GetItemsToSkip();
            var itemsToTake = request.GetItemsToTake();

            var result = await orderRepository.GetUserOrders(itemsToSkip, itemsToTake, request.UserId)
                .ConfigureAwait(false);

            return new PageModel<OrderModel>
            {
                Items = mapper.Map<OrderModel[]>(result.Items),
                TotalNumberOfItems = result.TotalNumberOfItems
            };
        }
    }
}
