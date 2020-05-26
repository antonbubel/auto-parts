namespace AutoParts.Core.Implementation.Orders.RequestHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Orders.Models;
    using Contracts.Orders.Requests;

    using Data.Model.Enums;
    using Data.Model.Repositories;

    using Infrastructure.Exceptions;

    public class GetOrderByIdRequestHandler : IRequestHandler<GetOrderByIdRequest, OrderModel>
    {
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IAutoPartRepository autoPartRepository;
        private readonly IUserRepository userRepository;

        public GetOrderByIdRequestHandler(
            IMapper mapper,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IAutoPartRepository autoPartRepository,
            IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            this.autoPartRepository = autoPartRepository;
            this.userRepository = userRepository;
        }

        public async Task<OrderModel> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(GetOrderByIdRequest)} argument cannot be null.");
            }

            var order = await orderRepository.FindAsync(request.OrderId)
                .ConfigureAwait(false);

            if (order == null)
            {
                throw new NotFoundException();
            }

            var user = await userRepository.FindAsync(request.UserId)
                .ConfigureAwait(false);

            if (user.UserTypeId == UserType.User && order.UserId != user.Id)
            {
                throw new ForbiddenException();
            }

            if (user.UserTypeId == UserType.Supplier)
            {
                var supplierCanViewOrder = await VerifySupplierCanViewOrder(user.Id, request.OrderId);

                if (!supplierCanViewOrder)
                {
                    throw new ForbiddenException();
                }
            }

            return mapper.Map<OrderModel>(order);
        }

        private async Task<bool> VerifySupplierCanViewOrder(long supplierId, long orderId)
        {
            var orderItems = await orderItemRepository.GetOrderItems(orderId)
                .ConfigureAwait(false);

            var autoPartIds = orderItems.Select(orderItem => orderItem.AutoPartId)
                .ToArray();

            var suppliers = await autoPartRepository.GetAutoPartsSuppliers(autoPartIds)
                .ConfigureAwait(false);

            return suppliers.Any(supplier => supplier.Id == supplierId);
        }
    }
}
