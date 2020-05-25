namespace AutoParts.Core.Implementation.AutoParts.NotificationHandlers
{
    using MediatR;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.AutoParts.Notifications;

    using Data.Model.Repositories;

    using Infrastructure.Exceptions;

    public class UpdateAutoPartAvailabilityNotificationHandler : INotificationHandler<UpdateAutoPartAvailabilityNotification>
    {
        private readonly IAutoPartRepository autoPartRepository;

        public UpdateAutoPartAvailabilityNotificationHandler(IAutoPartRepository autoPartRepository)
        {
            this.autoPartRepository = autoPartRepository;
        }

        public async Task Handle(UpdateAutoPartAvailabilityNotification notification, CancellationToken cancellationToken)
        {
            var autoPart = await autoPartRepository.FindAsync(notification.AutoPartId)
                .ConfigureAwait(false);

            if (autoPart == null)
            {
                throw new NotFoundException();
            }

            autoPart.Quantity -= notification.RemovedQuantity;

            if (autoPart.Quantity <= 0)
            {
                autoPart.IsAvailable = false;
            }

            await autoPartRepository.UpdateAsync(autoPart)
                .ConfigureAwait(false);
        }
    }
}
