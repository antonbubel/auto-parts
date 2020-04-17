namespace AutoParts.Core.Implementation.CarModifications.NotificationHandlers
{
    using MediatR;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarModifications.Exceptions;
    using Contracts.CarModifications.Notifications;

    using Data.Model.Results;
    using Data.Model.Repositories;

    public class DeleteCarModificationNotificationHandler : INotificationHandler<DeleteCarModificationNotification>
    {
        private readonly ICarModificationRepository carModificationRepository;

        public DeleteCarModificationNotificationHandler(ICarModificationRepository carModificationRepository)
        {
            this.carModificationRepository = carModificationRepository;
        }

        public async Task Handle(DeleteCarModificationNotification notification, CancellationToken cancellationToken)
        {
            var operationResult = await carModificationRepository.RemoveAsync(notification.CarModificationId)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new DeleteCarModificationException(operationResult);
            }
        }
    }
}
