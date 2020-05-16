namespace AutoParts.Core.Implementation.Orders.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Orders.Notifications;

    using Data.Model.Results;
    using Data.Model.Entities;
    using Data.Model.Repositories;

    public class CreateOrderNotificationHandler : INotificationHandler<CreateOrderNotification>
    {
        public Task Handle(CreateOrderNotification notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
