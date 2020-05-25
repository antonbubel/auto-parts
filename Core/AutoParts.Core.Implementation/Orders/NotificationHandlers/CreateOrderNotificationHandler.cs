namespace AutoParts.Core.Implementation.Orders.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Orders.Models;
    using Contracts.Orders.Exceptions;
    using Contracts.Orders.Notifications;

    using Contracts.AutoParts.Notifications;

    using Contracts.Emails.Notifications;

    using Data.Model.Results;
    using Data.Model.Entities;
    using Data.Model.Repositories;

    public class CreateOrderNotificationHandler : INotificationHandler<CreateOrderNotification>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;
        private readonly IAutoPartRepository autoPartRepository;

        public CreateOrderNotificationHandler(
            IMediator mediator,
            IMapper mapper,
            IOrderRepository orderRepository,
            IAutoPartRepository autoPartRepository)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.orderRepository = orderRepository;
            this.autoPartRepository = autoPartRepository;
        }

        public async Task Handle(CreateOrderNotification notification, CancellationToken cancellationToken)
        {
            var order = mapper.Map<Order>(notification);
            var operationResult = await orderRepository.CreateAsync(order)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new CreateOrderException(operationResult);
            }

            await UpdateAutoPartsAvailability(notification.OrderItems);
            await SendOrderCreatedNotificationToUser(order);
            await SendOrderCreatedNotificationToSuppliers(notification.OrderItems);
        }

        private async Task UpdateAutoPartsAvailability(OrderItemModel[] orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                var notification = new UpdateAutoPartAvailabilityNotification
                {
                    AutoPartId = orderItem.AutoPartId,
                    RemovedQuantity = orderItem.Quantity
                };

                await mediator.Publish(notification);
            }
        }

        private async Task SendOrderCreatedNotificationToUser(Order order)
        {
            var notification = new SendUserOrderCreatedEmailNotification
            {
                ToEmail = order.Email,
                ToName = $"{order.FirstName} {order.LastName}"
            };

            await mediator.Publish(notification);
        }

        private async Task SendOrderCreatedNotificationToSuppliers(OrderItemModel[] orderItems)
        {
            var autoPartIds = orderItems.Select(orderItem => orderItem.AutoPartId)
                .ToArray();

            var autoPartsSuppliers = await autoPartRepository.GetAutoPartsSuppliers(autoPartIds)
                .ConfigureAwait(false);

            foreach (var supplier in autoPartsSuppliers)
            {
                var notification = new SendSupplierOrderCreatedEmailNotification
                {
                    ToEmail = supplier.Email,
                    ToName = $"{supplier.FirstName} {supplier.LastName}"
                };

                await mediator.Publish(notification);
            }
        }
    }
}
