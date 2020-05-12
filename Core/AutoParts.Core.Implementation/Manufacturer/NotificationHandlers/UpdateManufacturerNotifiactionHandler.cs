namespace AutoParts.Core.Implementation.Manufacturer.NotificationHandlers
{
    using MediatR;

    using AutoMapper;

    using System.Threading;
    using System.Threading.Tasks;

    using Contracts.Manufacturer.Exceptions;
    using Contracts.Manufacturer.Notifications;

    using Contracts.Files.Requests;
    using Contracts.Files.Notifications;

    using Data.Model.Results;
    using Data.Model.Repositories;

    using Infrastructure.Exceptions;

    public class UpdateManufacturerNotificationHandler : INotificationHandler<UpdateManufacturerNotification>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IManufacturerRepository manufacturerRepository;

        public UpdateManufacturerNotificationHandler(
            IMediator mediator,
            IMapper mapper,
            IManufacturerRepository manufacturerRepository)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.manufacturerRepository = manufacturerRepository;
        }

        public async Task Handle(UpdateManufacturerNotification notification, CancellationToken cancellationToken)
        {
            var manufacturer = await manufacturerRepository.FindAsync(notification.ManufacturerId);

            if (manufacturer == null)
            {
                throw new NotFoundException();
            }

            var carBrandImage = manufacturer.Image;

            if (!string.IsNullOrEmpty(notification.ImageFileName) && !notification.ImageFileBuffer.IsEmpty)
            {
                if (!string.IsNullOrEmpty(manufacturer.Image))
                {
                    await mediator.Publish(new DeleteFileNotification { FileName = manufacturer.Image });
                }

                carBrandImage = await mediator.Send(new SaveFileRequest { FileName = notification.ImageFileName, Buffer = notification.ImageFileBuffer });
            }

            mapper.Map(notification, manufacturer);

            manufacturer.Image = carBrandImage;

            var operationResult = await manufacturerRepository.UpdateAsync(manufacturer)
                .ConfigureAwait(false);

            if (operationResult.Status != OperationStatus.Successful)
            {
                throw new UpdateManufacturerException(operationResult);
            }
        }
    }
}
