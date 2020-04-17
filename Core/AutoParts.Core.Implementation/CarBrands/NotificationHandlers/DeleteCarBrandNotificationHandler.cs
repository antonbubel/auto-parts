namespace AutoParts.Core.Implementation.CarBrands.NotificationHandlers
{
    using MediatR;

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.CarBrands.Exceptions;
    using Contracts.CarBrands.Notifications;

    using Contracts.Files.Notifications;

    using Data.Model.Results;
    using Data.Model.Repositories;

    public class DeleteCarBrandNotificationHandler : INotificationHandler<DeleteCarBrandNotification>
    {
        private readonly IMediator mediator;
        private readonly ICarBrandRepository carBrandRepository;

        public DeleteCarBrandNotificationHandler(IMediator mediator, ICarBrandRepository carBrandRepository)
        {
            this.mediator = mediator;
            this.carBrandRepository = carBrandRepository;
        }

        public async Task Handle(DeleteCarBrandNotification notification, CancellationToken cancellationToken)
        {
            var carBrand = await carBrandRepository.FindAsync(notification.CarBrandId)
                .ConfigureAwait(false);

            if (carBrand == null)
            {
                // TODO: throw more concrete exception instead of System.ArgumentException
                throw new ArgumentException();
            }

            if (!string.IsNullOrEmpty(carBrand.Image))
            {
                await mediator.Publish(new DeleteFileNotification { FileName = carBrand.Image });
            }

            var operationResult = await carBrandRepository.RemoveAsync(notification.CarBrandId)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new DeleteCarBrandException(operationResult);
            }
        }
    }
}
