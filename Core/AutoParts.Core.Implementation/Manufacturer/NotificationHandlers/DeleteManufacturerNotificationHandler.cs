namespace AutoParts.Core.Implementation.Manufacturer.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Manufacturer.Exceptions;
    using Contracts.Manufacturer.Notifications;

    using Contracts.Files.Notifications;

    using Data.Model.Results;
    using Data.Model.Repositories;

    using Infrastructure.Exceptions;

    public class DeleteManufacturerNotificationHandler : INotificationHandler<DeleteManufacturerNotification>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IManufacturerRepository manufacturerRepository;

        public DeleteManufacturerNotificationHandler(
            IMediator mediator,
            IMapper mapper,
            IManufacturerRepository manufacturerRepository)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.manufacturerRepository = manufacturerRepository;
        }

        public async Task Handle(DeleteManufacturerNotification notification, CancellationToken cancellationToken)
        {
            var manufacturer = await manufacturerRepository.FindAsync(notification.ManufacturerId)
                .ConfigureAwait(false);

            if (manufacturer == null)
            {
                throw new NotFoundException();
            }

            if (!string.IsNullOrEmpty(manufacturer.Image))
            {
                await mediator.Publish(new DeleteFileNotification { FileName = manufacturer.Image });
            }

            var operationResult = await manufacturerRepository.RemoveAsync(notification.ManufacturerId)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new DeleteManufacturerException(operationResult);
            }
        }
    }
}
