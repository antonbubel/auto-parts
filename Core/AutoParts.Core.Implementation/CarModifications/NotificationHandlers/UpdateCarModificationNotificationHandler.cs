namespace AutoParts.Core.Implementation.CarModifications.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarModifications.Exceptions;
    using Contracts.CarModifications.Notifications;

    using Data.Model.Results;
    using Data.Model.Repositories;

    public class UpdateCarModificationNotificationHandler : INotificationHandler<UpdateCarModificationNotification>
    {
        private readonly IMapper mapper;
        private readonly ICarModificationRepository carModificationRepository;

        public UpdateCarModificationNotificationHandler(IMapper mapper, ICarModificationRepository carModificationRepository)
        {
            this.mapper = mapper;
            this.carModificationRepository = carModificationRepository;
        }

        public async Task Handle(UpdateCarModificationNotification notification, CancellationToken cancellationToken)
        {
            var carModification = await carModificationRepository.FindAsync(notification.CarModificationId)
                .ConfigureAwait(false);

            if (carModification == null)
            {
                // TODO: throw more concrete exception instead of System.ArgumentException
                throw new ArgumentException();
            }

            mapper.Map(notification, carModification);

            var operationResult = await carModificationRepository.UpdateAsync(carModification)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new UpdateCarModificationException(operationResult);
            }
        }
    }
}
