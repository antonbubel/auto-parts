namespace AutoParts.Core.Implementation.CarModels.NotificationHandlers
{
    using MediatR;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarModels.Exceptions;
    using Contracts.CarModels.Notifications;

    using Contracts.Files.Notifications;

    using Data.Model.Results;
    using Data.Model.Repositories;

    public class DeleteCarModelNotificationHandler : INotificationHandler<DeleteCarModelNotification>
    {
        private readonly IMediator mediator;
        private readonly ICarModelRepository carModelRepository;

        public DeleteCarModelNotificationHandler(IMediator mediator, ICarModelRepository carModelRepository)
        {
            this.mediator = mediator;
            this.carModelRepository = carModelRepository;
        }

        public async Task Handle(DeleteCarModelNotification notification, CancellationToken cancellationToken)
        {
            var carModel = await carModelRepository.FindAsync(notification.CarModelId)
                .ConfigureAwait(false);

            if (carModel == null)
            {
                // TODO: throw more concrete exception instead of System.ArgumentException
                throw new ArgumentException();
            }

            if (!string.IsNullOrEmpty(carModel.Image))
            {
                await mediator.Publish(new DeleteFileNotification { FileName = carModel.Image });
            }

            var operationResult = await carModelRepository.RemoveAsync(notification.CarModelId)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new DeleteCarModelException(operationResult);
            }
        }
    }
}
